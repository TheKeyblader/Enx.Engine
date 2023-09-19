namespace Enx.Engine.Asset;

public interface IAssetLoader<TAsset>
{
    IEnumerable<string> FileExtensions { get; }
    Task<TAsset> Load(Stream stream);
}
