using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Enx.Engine.Asset;

public class AssetManagerImpl : IAssetManager
{
    private readonly ConcurrentDictionary<HandleId, AssetInfo> _info = new();
    private readonly IServiceProvider _serviceProvider;

    public AssetManagerImpl(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Handle<TAsset> Add<TAsset>(TAsset asset)
    {
        var id = new HandleId();
        var info = new AssetInfo
        {
            HandleId = id,
            Asset = asset,
            LoadState = LoadState.Loaded
        };
        if (!_info.TryAdd(id, info))
            throw new InvalidOperationException();

        return new Handle<TAsset>(id, HandleType.Strong);
    }

    public TAsset GetAsset<TAsset>(HandleId id)
    {
        if (_info.TryGetValue(id, out var info))
        {
            if (info.LoadState != LoadState.Loaded)
                throw new InvalidOperationException();

            return (TAsset)info.Asset!;
        }
        else throw new InvalidOperationException();
    }

    public Handle<TAsset> GetHandle<TAsset>(HandleId id)
    {
        if (!_info.ContainsKey(id)) throw new InvalidOperationException();
        return new Handle<TAsset>(id, HandleType.Strong);
    }
    public LoadState GetLoadState(HandleId id)
    {
        if (!_info.TryGetValue(id, out var info)) throw new InvalidOperationException();
        return info.LoadState;
    }

    public Handle<TAsset> Load<TAsset>(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException();

        var id = new HandleId(path);
        if (_info.ContainsKey(id)) throw new InvalidOperationException();

        var loaders = _serviceProvider.GetServices<IAssetLoader<TAsset>>();
        if (!loaders.Any()) throw new InvalidOperationException();

        var info = new AssetInfo { HandleId = id };
        _info[id] = info;

        _ = Task.Run(() => LoadInternal(loaders, id, path));

        return new Handle<TAsset>(id, HandleType.Strong);
    }

    async Task LoadInternal<TAsset>(IEnumerable<IAssetLoader<TAsset>> loaders, HandleId id, string path)
    {
        _info[id] = _info[id] with { LoadState = LoadState.Loading };
        var ext = Path.GetExtension(path);
        using var stream = File.OpenRead(path);
        foreach (var loader in loaders)
        {
            try
            {
                if (!loader.FileExtensions.Contains(ext)) continue;

                stream.Seek(0, SeekOrigin.Begin);
                var asset = await loader.Load(stream);
                _info[id] = _info[id] with { Asset = asset, LoadState = LoadState.Loaded };
                return;
            }
            catch { }
        }

        _info[id] = _info[id] with { LoadState = LoadState.Failed };
    }

    public void SetAsset<TAsset>(HandleId id, TAsset asset)
    {
        if (!_info.TryGetValue(id, out var info)) throw new InvalidOperationException();
        _info[id] = info with { Asset = asset };
    }
    public Handle<TTransformedAsset> Transform<TAsset, TTransformedAsset>(HandleId id, bool removeAsset = true)
    {
        if (!_info.TryGetValue(id, out var info)) throw new InvalidOperationException();
        if (info.LoadState != LoadState.Loaded) throw new InvalidOperationException();

        var transformers = _serviceProvider.GetServices<IAssetTransformer<TAsset, TTransformedAsset>>();
        if (!transformers.Any()) throw new InvalidOperationException();

        var guid = new HandleId();
        var transformedInfo = new AssetInfo { HandleId = id };
        _info[guid] = transformedInfo;

        _ = Task.Run(() => TransformInternal(transformers, id, guid));

        return new Handle<TTransformedAsset>(guid, HandleType.Strong);
    }

    async Task TransformInternal<TAsset, TTransformedAsset>(
        IEnumerable<IAssetTransformer<TAsset, TTransformedAsset>> loaders, HandleId assetId, HandleId newAssetId)
    {
        _info[newAssetId] = _info[newAssetId] with { LoadState = LoadState.Loading };

        try
        {
            var asset = GetAsset<TAsset>(assetId);
            foreach (var transformer in loaders)
            {
                var transformed = await transformer.TransformAsync(asset);
                _info[newAssetId] = _info[newAssetId] with { Asset = transformed, LoadState = LoadState.Loaded };
                return;
            }
        }
        catch { }

        _info[newAssetId] = _info[newAssetId] with { LoadState = LoadState.Failed };
    }
}

public record struct AssetInfo
{
    public HandleId HandleId { get; set; }
    public LoadState LoadState { get; set; }
    public object? Asset { get; set; }
}
