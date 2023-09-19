namespace Enx.WebGPU;

public struct XBindGroupEntry
{
    public int Binding { get; set; }
    public XBuffer? Buffer { get; set; }
    public long Offset { get; set; }
    public long Size { get; set; }
    public XSampler? Sampler { get; set; }
    public XTextureView? TextureView { get; set; }
}
