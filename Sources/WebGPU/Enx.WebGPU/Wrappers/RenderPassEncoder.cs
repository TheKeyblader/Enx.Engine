using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(RenderPassEncoder))]
public readonly partial struct XRenderPassEncoder
{
    public void SetPipeline(XRenderPipeline pipeline)
    {
        unsafe
        {
            Api.RenderPassEncoderSetPipeline(Handle, pipeline.Handle);
        }
    }

    public void SetIndexBuffer(XBuffer buffer, IndexFormat format, long offset = 0, long? size = null)
    {
        unsafe
        {
            Api.RenderPassEncoderSetIndexBuffer(Handle, buffer.Handle, format, (ulong)offset, (ulong)(size ?? buffer.Size));
        }
    }
    public void SetVertexBuffer(int slot, XBuffer buffer, long offset = 0, long? size = null)
    {
        unsafe
        {
            Api.RenderPassEncoderSetVertexBuffer(Handle, (uint)slot, buffer.Handle, (ulong)offset, (ulong)(size ?? buffer.Size));
        }
    }
    public void SetBindGroup(int groupIndex, XBindGroup bindGroup, Span<int> dynamicOffset = default)
    {
        unsafe
        {
            var dynamicOffsets = stackalloc uint[dynamicOffset.Length];
            for (int i = 0; i < dynamicOffset.Length; i++)
                dynamicOffsets[i] = (uint)dynamicOffset[i];

            Api.RenderPassEncoderSetBindGroup(Handle, (uint)groupIndex, bindGroup.Handle, (nuint)dynamicOffset.Length, dynamicOffsets);
        }
    }

    public void Draw(int vertexCount, int instanceCount, int firstVertex, int firstInstance)
    {
        unsafe
        {
            Api.RenderPassEncoderDraw(Handle, (uint)vertexCount, (uint)instanceCount, (uint)firstVertex, (uint)firstInstance);
        }
    }
    public void DrawIndexed(int indexCount, int instanceCount, int firstIndex, int baseVertex, int firstInstance)
    {
        unsafe
        {
            Api.RenderPassEncoderDrawIndexed(Handle, (uint)indexCount, (uint)instanceCount, (uint)firstIndex, baseVertex, (uint)firstInstance);
        }
    }
    public void End()
    {
        unsafe
        {
            Api.RenderPassEncoderEnd(Handle);
        }
    }

    partial void DisposeInternal()
    {
        End();
    }
}
