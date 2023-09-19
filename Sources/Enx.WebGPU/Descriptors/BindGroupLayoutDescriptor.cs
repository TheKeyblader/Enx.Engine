namespace Enx.WebGPU;

public ref struct XBindGroupLayoutDescriptor
{
    public string Label { get; set; }
    public Span<XBindGroupLayoutEntry> Entries { get; set; }
}
