namespace Enx.WebGPU;

public ref struct XTextureDescriptor
{
    public string Label { get; set; }
    public TextureUsage Usage { get; set; }
    public TextureDimension Dimension { get; set; }
    public XExtent3D Size { get; set; }
    public TextureFormat Format { get; set; }
    public int MipLevelCount { get; set; }
    public int SampleCount { get; set; }
    public Span<TextureFormat> ViewFormats { get; set; }
}
