namespace Enx.WebGPU;

public struct XTextureViewDescriptor
{
    public string Label { get; set; }
    public TextureFormat Format { get; set; }
    public TextureViewDimension Dimension { get; set; }
    public int BaseMipLevel { get; set; }
    public int MipLvelCount { get; set; }
    public int BaseArrayLayer { get; set; }
    public int ArrayLayerCount { get; set; }
    public TextureAspect Aspect { get; set; }
}
