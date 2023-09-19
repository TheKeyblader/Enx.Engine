using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(BindGroup))]
public readonly partial struct XBindGroup
{
    public string Label
    {
        set
        {
            unsafe
            {
                Api.BindGroupSetLabel(Handle, value);
            }
        }
    }
}
