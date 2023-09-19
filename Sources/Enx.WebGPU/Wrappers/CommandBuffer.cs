using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(CommandBuffer))]
public readonly partial struct XCommandBuffer
{
    public string Label
    {
        set
        {
            unsafe
            {
                Api.CommandBufferSetLabel(Handle, value);
            }
        }
    }
}
