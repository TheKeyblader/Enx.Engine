namespace Enx.WebGPU;

public ref struct XRenderPassDescriptor
{
    public string Label { get; set; }
    public Span<XRenderPassColorAttachment> ColorAttachements { get; set; }
    public XRenderPassDepthStencilAttachment? DepthStencilAttachment { get; set; }
    public XQuerySet? OcclusionQuerySet { get; set; }
    public XRenderPassTimestampWrite[] TimestampWrites { get; set; }
}

public struct XRenderPassTimestampWrite
{
    public XQuerySet QuerySet { get; set; }
    public int QueryIndex { get; set; }
    public RenderPassTimestampLocation Location { get; set; }
}
