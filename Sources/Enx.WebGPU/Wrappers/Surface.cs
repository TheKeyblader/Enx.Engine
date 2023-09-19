using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(Surface))]
public readonly partial struct XSurface
{
    public TextureFormat GetPreferredFormat(XAdapter adapter)
    {
        unsafe
        {
            return Api.SurfaceGetPreferredFormat(Handle, adapter.Handle);
        }
    }
}
