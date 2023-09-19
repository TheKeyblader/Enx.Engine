namespace Enx.WebGPU;

public ref struct XBindGroupDescriptor
{
    public string Label { get; set; }
    public XBindGroupLayout Layout { get; set; }
    public Span<XBindGroupEntry> Entries { get; set; }
}
