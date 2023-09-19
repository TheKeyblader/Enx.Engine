namespace Enx.Engine.Asset;

public interface IAssetTransformer<TAsset, TTransformedAsset>
{
    Task<TTransformedAsset> TransformAsync(TAsset asset);
}
