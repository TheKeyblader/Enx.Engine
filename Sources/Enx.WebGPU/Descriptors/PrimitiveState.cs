namespace Enx.WebGPU;

public struct XPrimitiveState
{
    public PrimitiveTopology Topology { get; set; }
    public IndexFormat StripIndexFormat { get; set; }
    public FrontFace FrontFace { get; set; }
    public CullMode CullMode { get; set; }
}
