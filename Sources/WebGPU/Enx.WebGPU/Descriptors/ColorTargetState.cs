namespace Enx.WebGPU;

public struct XColorTargetState
{
    public TextureFormat Format { get; set; }
    public XBlendState Blend { get; set; }
    public ColorWriteMask WriteMask { get; set; }
}
