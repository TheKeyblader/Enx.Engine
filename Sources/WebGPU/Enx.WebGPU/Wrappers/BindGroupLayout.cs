using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(BindGroupLayout))]
public readonly partial struct XBindGroupLayout
{
    public string Label
    {
        set
        {
            unsafe
            {
                Api.BindGroupLayoutSetLabel(Handle, value);
            }
        }
    }
}
