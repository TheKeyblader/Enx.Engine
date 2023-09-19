namespace Enx.WebGPU;

public struct XFragmentState
{
    public XShaderModule Module { get; set; }
    public string EntryPoint { get; set; }
    public Dictionary<string, double> Constants { get; set; }
    public XColorTargetState[] Targets { get; set; }
}
