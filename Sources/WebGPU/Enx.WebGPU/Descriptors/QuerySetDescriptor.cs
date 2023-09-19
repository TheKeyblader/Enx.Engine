namespace Enx.WebGPU;

public struct XQuerySetDescriptor
{
    public string Label { get; set; }
    public QueryType Type { get; set; }
    public int Count { get; set; }
    public PipelineStatisticName[] PipelineStatistics { get; set; }
}
