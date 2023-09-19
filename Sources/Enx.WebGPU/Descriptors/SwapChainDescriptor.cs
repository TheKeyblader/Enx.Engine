namespace Enx.WebGPU;

public struct XSwapChainDescriptor
{
    public string Label { get; set; }
    public TextureUsage Usage { get; set; }
    public TextureFormat Format { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public PresentMode PresentMode { get; set; }
}
