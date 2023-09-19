namespace Enx.Engine.Asset;

public interface IAssetManager
{
    public Handle<TAsset> Add<TAsset>(TAsset asset);
    public Handle<TAsset> Load<TAsset>(string path);
    public Handle<TAsset> GetHandle<TAsset>(HandleId id);
    public LoadState GetLoadState(HandleId id);

    /// <summary>
    /// Transfrom a asset in another one like a image into texture
    /// </summary>
    /// <typeparam name="TAsset">Asset to transform</typeparam>
    /// <typeparam name="TTransformedAsset">transformed asset</typeparam>
    /// <param name="id">id of asset</param>
    /// <param name="removeAsset">when false allows asset to be transformed multiple times</param>
    /// <returns>transformed asset</returns>
    public Handle<TTransformedAsset> Transform<TAsset, TTransformedAsset>(HandleId id, bool removeAsset = true);
    public TAsset GetAsset<TAsset>(HandleId id);
    public void SetAsset<TAsset>(HandleId id, TAsset asset);
}

public enum LoadState
{
    NotLoaded,
    Loading,
    Loaded,
    Failed,
    Unloaded
}
