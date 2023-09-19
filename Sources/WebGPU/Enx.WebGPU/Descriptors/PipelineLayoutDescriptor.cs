namespace Enx.WebGPU;

public ref struct XPipelineLayoutDescriptor
{
    public string Label { get; set; }
    public Span<XBindGroupLayout> BindGroupLayouts { get; set; }
}
