namespace Enx.WebGPU;

public struct XRenderPassDepthStencilAttachment
{
    public XTextureView View { get; set; }
    public LoadOp DepthLoadOp { get; set; }
    public StoreOp DepthStoreOp { get; set; }
    public float DepthClearValue { get; set; }
    public bool DepthReadOnly { get; set; }
    public LoadOp StencilLoadOp { get; set; }
    public StoreOp StencilStoreOp { get; set; }
    public int StencilClearValue { get; set; }
    public bool StencilReadOnly { get; set; }
}
