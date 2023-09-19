namespace Enx.WebGPU;

public struct XRenderBundleEncoderDescriptor
{
    public string Label { get; set; }
    public TextureFormat[] ColorFormats { get; set; }
    public int SampleCount { get; set; }
    public bool DepthReadOnly { get; set; }
    public bool StencilReadOnly { get; set; }
}
