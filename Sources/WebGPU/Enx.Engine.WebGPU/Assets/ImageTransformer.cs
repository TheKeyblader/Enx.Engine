
using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Asset;
using Enx.Engine.Graphics.Assets;
using Enx.Engine.WebGPU.Components;
using Enx.WebGPU;
using Silk.NET.WebGPU;

namespace Enx.Engine.WebGPU.Assets;
public class ImageTransformer(World world) : IAssetTransformer<Image, GpuImage>
{
    private readonly World _world = world;

    public Task<GpuImage> TransformAsync(Image asset)
    {
        if (asset is not ImageImpl impl) throw new InvalidOperationException();

        var device = _world.QueryUnique<XDevice>();

        var texture = device.CreateTexture(new XTextureDescriptor
        {
            Dimension = TextureDimension.Dimension2D,
            Format = TextureFormat.Rgba8UnormSrgb,
            MipLevelCount = 1,
            SampleCount = 1,
            Size = new XExtent3D
            {
                Width = asset.Size.X,
                Height = asset.Size.Y,
                DepthOrArrayLayers = 1
            },
            Usage = TextureUsage.TextureBinding | TextureUsage.CopyDst,
        });

        device.Queue.WriteTexture<byte>(new XImageCopyTexture
        {
            Aspect = TextureAspect.All,
            MipLevel = 0,
            Texture = texture
        },
        new XTextureDataLayout
        {
            Offset = 0,
            BytesPerRow = 4 * asset.Size.X,
            RowsPerImage = asset.Size.Y,
        },
        new XExtent3D
        {
            Width = asset.Size.X,
            Height = asset.Size.Y,
            DepthOrArrayLayers = 1
        }, 0, impl.Data);

        var textureView = texture.CreateView(new XTextureViewDescriptor
        {
            Aspect = TextureAspect.All,
            BaseArrayLayer = 0,
            Format = TextureFormat.Rgba8UnormSrgb,
            Dimension = TextureViewDimension.Dimension2D,
            MipLvelCount = 1,
            ArrayLayerCount = 1,
        });

        return Task.FromResult(new GpuImage
        {
            Size = asset.Size,
            Texture = texture,
            TextureView = textureView,
        });
    }
}
