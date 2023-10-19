using Enx.WebGPU.SourceGenerator;
using Silk.NET.WebGPU;

namespace Enx.WebGPU;

[Wrapper(typeof(Queue))]
public readonly partial struct XQueue
{
    public Action<QueueWorkDoneStatus> WorkSubmitted
    {
        set
        {
            unsafe
            {
                Api.QueueOnSubmittedWorkDone(Handle, PfnQueueWorkDoneCallback.From((QueueWorkDoneStatus arg0, void* arg1) => value(arg0)), null);
            }
        }
    }

    public void Submit(XCommandBuffer commandBuffer)
    {
        Span<XCommandBuffer> buffers = [commandBuffer];
        Submit(in buffers);
    }

    public void WriteBuffer<T0>(XBuffer buffer, int offset, Span<T0> data)
        where T0 : unmanaged
    {
        unsafe
        {
            fixed (void* ptr = data)
            {
                var size = UnsafeHelpers.BufferAlign<T0>();
                Api.QueueWriteBuffer(Handle, buffer.Handle, (uint)(offset * size), ptr, (uint)(size * data.Length));
            }
        }
    }
    public void WriteTexture<T0>(XImageCopyTexture imageCopyTexture, XTextureDataLayout textureDataLayout, XExtent3D extent3D, int offset, Span<T0> data) where T0 : unmanaged
    {
        unsafe
        {
            fixed (void* ptr = data)
            {
                var size = UnsafeHelpers.BufferAlign<T0>();
                Api.QueueWriteTexture(Handle, new ImageCopyTexture
                {
                    Aspect = imageCopyTexture.Aspect,
                    MipLevel = (uint)imageCopyTexture.MipLevel,
                    Origin = new Origin3D
                    {
                        X = (uint)imageCopyTexture.Origin.X,
                        Y = (uint)imageCopyTexture.Origin.Y,
                        Z = (uint)imageCopyTexture.Origin.Z,
                    },
                    Texture = imageCopyTexture.Texture.Handle
                },
                ptr, (nuint)(size * data.Length),
                new TextureDataLayout
                {
                    Offset = (ulong)textureDataLayout.Offset,
                    BytesPerRow = (uint)textureDataLayout.BytesPerRow,
                    RowsPerImage = (uint)textureDataLayout.RowsPerImage
                },
                new Extent3D
                {
                    DepthOrArrayLayers = (uint)extent3D.DepthOrArrayLayers,
                    Height = (uint)extent3D.Height,
                    Width = (uint)extent3D.Width
                });
            }
        }
    }

    public void Submit(in Span<XCommandBuffer> commandBuffers)
    {
        unsafe
        {
            var buffers = stackalloc CommandBuffer*[commandBuffers.Length];
            for (int i = 0; i < commandBuffers.Length; i++)
            {
                buffers[i] = commandBuffers[i].Handle;
            }
            Api.QueueSubmit(Handle, (uint)commandBuffers.Length, buffers);
        }
    }

    public unsafe static implicit operator Queue*(XQueue wrapper) => wrapper.Handle;
}
