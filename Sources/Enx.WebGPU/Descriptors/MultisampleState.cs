namespace Enx.WebGPU;

public struct XMultisampleState
{
    public int Count { get; set; }
    public uint Mask { get; set; }
    public bool AlphaToCoverageEnabled { get; set; }
}
