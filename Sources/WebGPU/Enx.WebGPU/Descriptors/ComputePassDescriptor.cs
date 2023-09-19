namespace Enx.WebGPU;

public struct XComputePassDescriptor
{
    public string Label { get; set; }
    public XComputePassTimestampWrite[] TimestampWrites { get; set; }
}

public struct XComputePassTimestampWrite
{
    public XQuerySet QuerySet { get; set; }
    public int QueryIndex { get; set; }
    public ComputePassTimestampLocation Location { get; set; }
}
