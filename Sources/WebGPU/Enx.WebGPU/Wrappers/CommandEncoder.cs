using Enx.WebGPU.SourceGenerator;
using Silk.NET.Core.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Enx.WebGPU;

[Wrapper(typeof(CommandEncoder))]
public readonly partial struct XCommandEncoder
{
    public XComputePassEncoder BeginComputePass(XComputePassDescriptor descriptor)
    {
        unsafe
        {
            var computePass = new ComputePassDescriptor();
            if (descriptor.Label is not null)
                computePass.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.TimestampWrites is not null && descriptor.TimestampWrites.Length != 0)
            {
                var writes = descriptor.TimestampWrites.Select(c => new ComputePassTimestampWrite
                {
                    Location = c.Location,
                    QueryIndex = (uint)c.QueryIndex,
                    QuerySet = c.QuerySet.Handle
                }).ToArray();

                fixed (ComputePassTimestampWrite* ptr = writes)
                {
                    computePass.TimestampWrites = ptr;
                    computePass.TimestampWriteCount = (uint)writes.Length;
                }
            }

            var handle = Api.CommandEncoderBeginComputePass(Handle, computePass);
            return new XComputePassEncoder(handle);
        }
    }
    public XRenderPassEncoder BeginRenderPass(XRenderPassDescriptor descriptor)
    {
        unsafe
        {
            var renderPass = new RenderPassDescriptor();

            if (descriptor.Label is not null)
                renderPass.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.OcclusionQuerySet.HasValue)
                renderPass.OcclusionQuerySet = descriptor.OcclusionQuerySet.Value.Handle;

            var attachment = new RenderPassDepthStencilAttachment();
            if (descriptor.DepthStencilAttachment.HasValue)
            {
                attachment.View = descriptor.DepthStencilAttachment.Value.View.Handle;
                attachment.DepthLoadOp = descriptor.DepthStencilAttachment.Value.DepthLoadOp;
                attachment.DepthStoreOp = descriptor.DepthStencilAttachment.Value.DepthStoreOp;
                attachment.DepthClearValue = descriptor.DepthStencilAttachment.Value.DepthClearValue;
                attachment.DepthReadOnly = descriptor.DepthStencilAttachment.Value.DepthReadOnly;
                attachment.StencilClearValue = (uint)descriptor.DepthStencilAttachment.Value.StencilClearValue;
                attachment.StencilLoadOp = descriptor.DepthStencilAttachment.Value.StencilLoadOp;
                attachment.StencilStoreOp = descriptor.DepthStencilAttachment.Value.StencilStoreOp;
                attachment.StencilReadOnly = descriptor.DepthStencilAttachment.Value.StencilReadOnly;
                renderPass.DepthStencilAttachment = &attachment;
            }

            if (descriptor.TimestampWrites is not null && descriptor.TimestampWrites.Length != 0)
            {
                var writes = descriptor.TimestampWrites.Select(c => new RenderPassTimestampWrite
                {
                    Location = c.Location,
                    QueryIndex = (uint)c.QueryIndex,
                    QuerySet = c.QuerySet.Handle
                }).ToArray();

                fixed (RenderPassTimestampWrite* ptr = writes)
                {
                    renderPass.TimestampWrites = ptr;
                    renderPass.TimestampWriteCount = (uint)writes.Length;
                }
            }

            if (descriptor.ColorAttachements.Length != 0)
            {
                var colors = stackalloc RenderPassColorAttachment[descriptor.ColorAttachements.Length];
                for (var i = 0; i < descriptor.ColorAttachements.Length; i++)
                {
                    var colorDescriptor = descriptor.ColorAttachements[i];
                    var color = new RenderPassColorAttachment()
                    {
                        ClearValue = colorDescriptor.ClearValue,
                        LoadOp = colorDescriptor.LoadOp,
                        ResolveTarget = colorDescriptor.ResolveTarget.Handle,
                        StoreOp = colorDescriptor.StoreOp,
                        View = colorDescriptor.View.Handle
                    };
                    colors[i] = color;
                }

                renderPass.ColorAttachments = colors;
                renderPass.ColorAttachmentCount = (uint)descriptor.ColorAttachements.Length;
            }

            var handle = Api.CommandEncoderBeginRenderPass(Handle, renderPass);
            return new(handle);
        }
    }
    public void ClearBuffer(XBuffer buffer, long offset, long size)
    {
        unsafe
        {
            Api.CommandEncoderClearBuffer(Handle, buffer.Handle, (ulong)offset, (ulong)size);
        }
    }
    public void CopyBufferToBuffer(XBuffer source, long sourceOffset, XBuffer destination, long destinationOffset, long size)
    {
        unsafe
        {
            Api.CommandEncoderCopyBufferToBuffer(Handle, source.Handle, (ulong)sourceOffset, destination.Handle, (ulong)destinationOffset, (ulong)size);
        }
    }
    public void CopyBufferToTexture(XImageCopyBuffer source, XImageCopyTexture destination, XExtent3D copySize)
    {
        unsafe
        {
            Api.CommandEncoderCopyBufferToTexture(Handle, new ImageCopyBuffer
            {
                Buffer = source.Buffer.Handle,
                Layout = new TextureDataLayout
                {
                    BytesPerRow = (uint)source.Layout.BytesPerRow,
                    Offset = (ulong)source.Layout.Offset,
                    RowsPerImage = (uint)source.Layout.RowsPerImage
                }
            }, new ImageCopyTexture
            {
                Aspect = destination.Aspect,
                MipLevel = (uint)destination.MipLevel,
                Origin = new Origin3D((uint)destination.Origin.X, (uint)destination.Origin.Y, (uint)destination.Origin.Z),
                Texture = destination.Texture.Handle,
            }, new Extent3D
            {
                DepthOrArrayLayers = (uint)copySize.DepthOrArrayLayers,
                Height = (uint)copySize.Height,
                Width = (uint)copySize.Width,
            });
        }
    }
    public void CopyTextureToBuffer(XImageCopyTexture source, XImageCopyBuffer destination, XExtent3D copySize)
    {
        unsafe
        {
            Api.CommandEncoderCopyTextureToBuffer(Handle, new ImageCopyTexture
            {
                Aspect = source.Aspect,
                MipLevel = (uint)source.MipLevel,
                Origin = new Origin3D((uint)source.Origin.X, (uint)source.Origin.Y, (uint)source.Origin.Z),
                Texture = source.Texture.Handle,
            }, new ImageCopyBuffer
            {
                Buffer = destination.Buffer.Handle,
                Layout = new TextureDataLayout
                {
                    BytesPerRow = (uint)destination.Layout.BytesPerRow,
                    Offset = (ulong)destination.Layout.Offset,
                    RowsPerImage = (uint)destination.Layout.RowsPerImage
                }
            },
            new Extent3D
            {
                DepthOrArrayLayers = (uint)copySize.DepthOrArrayLayers,
                Height = (uint)copySize.Height,
                Width = (uint)copySize.Width,
            });
        }
    }
    public XCommandBuffer Finish(string? commandBufferName = null)
    {
        unsafe
        {
            var desc = new CommandBufferDescriptor
            {
                Label = (byte*)SilkMarshal.StringToPtr(commandBufferName)
            };
            var handle = Api.CommandEncoderFinish(Handle, desc);
            return new XCommandBuffer(handle);
        }
    }
}
