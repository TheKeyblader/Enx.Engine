using Enx.WebGPU.SourceGenerator;
using Silk.NET.Core.Native;

namespace Enx.WebGPU;

[Wrapper(typeof(Texture))]
public readonly partial struct XTexture
{
    public int DepthOrLayers
    {
        get
        {
            unsafe
            {
                return (int)Api.TextureGetDepthOrArrayLayers(Handle);
            }
        }
    }
    public TextureDimension Dimension
    {
        get
        {
            unsafe
            {
                return Api.TextureGetDimension(Handle);
            }
        }
    }
    public TextureFormat Format
    {
        get
        {
            unsafe
            {
                return Api.TextureGetFormat(Handle);
            }
        }
    }
    public int Height
    {
        get
        {
            unsafe
            {
                return (int)Api.TextureGetHeight(Handle);
            }
        }
    }
    public int MipLevelCount
    {
        get
        {
            unsafe
            {
                return (int)Api.TextureGetMipLevelCount(Handle);
            }
        }
    }
    public int SampleCount
    {
        get
        {
            unsafe
            {
                return (int)Api.TextureGetSampleCount(Handle);
            }
        }
    }
    public TextureUsage Usage
    {
        get
        {
            unsafe
            {
                return Api.TextureGetUsage(Handle);
            }
        }
    }
    public int Width
    {
        get
        {
            unsafe
            {
                return (int)Api.TextureGetWidth(Handle);
            }
        }
    }


    public XTextureView CreateView(XTextureViewDescriptor descriptor)
    {
        unsafe
        {
            var textureView = new TextureViewDescriptor()
            {
                ArrayLayerCount = (uint)descriptor.ArrayLayerCount,
                Aspect = descriptor.Aspect,
                Dimension = descriptor.Dimension,
                Format = descriptor.Format,
                BaseArrayLayer = (uint)descriptor.BaseArrayLayer,
                BaseMipLevel = (uint)descriptor.BaseMipLevel,
                MipLevelCount = (uint)descriptor.MipLvelCount,
            };

            if (!string.IsNullOrEmpty(descriptor.Label))
                textureView.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            var handle = Api.TextureCreateView(Handle, textureView);
            return new XTextureView(handle);
        }
    }
}
