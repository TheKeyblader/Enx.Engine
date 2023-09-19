namespace Enx.WebGPU;

public unsafe struct XRenderPassColorAttachment
{
    public XTextureView View { get; set; }
    public XTextureView ResolveTarget { get; set; }
    public LoadOp LoadOp { get; set; }
    public StoreOp StoreOp { get; set; }
    public Color ClearValue { get; set; }
}
