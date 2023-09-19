namespace Enx.WebGPU;

public struct XComputePipelineDescriptor
{
    public string Label { get; set; }
    public XPipelineLayout Layout { get; set; }
    public XProgrammableStageDescriptor Compute { get; set; }
}

public struct XProgrammableStageDescriptor
{
    public XShaderModule Module { get; set; }
    public string EntryPoint { get; set; }
    public IReadOnlyDictionary<string, double> Contants { get; set; }
}
