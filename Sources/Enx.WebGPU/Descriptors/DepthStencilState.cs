namespace Enx.WebGPU;

public struct XDepthStencilState
{
    public TextureFormat Format { get; set; }
    public bool DepthWriteEnabled { get; set; }
    public CompareFunction DepthCompare { get; set; }
    public XStencilFaceState StencilFront { get; set; }
    public XStencilFaceState StencilBack { get; set; }
    public int StencilReadMask { get; set; }
    public int StencilWriteMask { get; set; }
    public int DepthBias { get; set; }
    public float DepthBiasSlopeScale { get; set; }
    public float DepthBiasClamp { get; set; }
}

public struct XStencilFaceState
{
    public CompareFunction Compare { get; set; }
    public StencilOperation FailOp { get; set; }
    public StencilOperation DepthFailOp { get; set; }
    public StencilOperation PassOp { get; set; }

    public StencilFaceState ToNative()
    {
        return new StencilFaceState
        {
            Compare = Compare,
            FailOp = FailOp,
            DepthFailOp = DepthFailOp,
            PassOp = PassOp,
        };
    }
}
