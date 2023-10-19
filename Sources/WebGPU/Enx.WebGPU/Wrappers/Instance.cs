using Enx.WebGPU.SourceGenerator;
using Silk.NET.Core.Contexts;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Enx.WebGPU;

[Wrapper(typeof(Instance))]
public readonly partial struct XInstance
{
    public static XInstance Create()
    {
        unsafe
        {
            var extras = new InstanceExtras(new ChainedStruct(null, (SType)NativeSType.STypeInstanceExtras), InstanceBackend.DX12);
            var handle = Api.CreateInstance(new InstanceDescriptor()
            {
                NextInChain = (ChainedStruct*)&extras
            });
            return new XInstance(handle);
        }
    }

    public XSurface CreateSurface(INativeWindowSource source)
    {
        unsafe
        {
            var handle = WebGPUSurface.CreateWebGPUSurface(source, Api, Handle);
            return new XSurface(handle);
        }
    }

    public void ProcessEvents()
    {
        unsafe
        {
            Api.InstanceProcessEvents(Handle);
        }
    }

    public XAdapter RequestAdapter(XRequestAdapterOptions options)
    {
        unsafe
        {
            Adapter* handle = null;
            Api.InstanceRequestAdapter(Handle, new RequestAdapterOptions
            {
                CompatibleSurface = options.CompatibleSurface.HasValue ? options.CompatibleSurface.Value.Handle : null,
                ForceFallbackAdapter = options.ForceFallbackAdapter,
                PowerPreference = options.PowerPreference,
            }, PfnRequestAdapterCallback.From((RequestAdapterStatus arg0, Adapter* arg1, byte* arg2, void* arg3) =>
            {
                handle = arg1;
            }), null);

            return new XAdapter(handle);
        }
    }
}
