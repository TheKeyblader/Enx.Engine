using Silk.NET.Core.Native;
using Silk.NET.WebGPU.Extensions.WGPU;

//Wrapped = Full Name Wrapped Type
//Wraped = Wrapped Type Name
//WrapperStruct = Wrapper Type Name
//XNamespace = namespace of Wrapper Type Name


namespace XNamespace;

public partial struct WrapperStruct : IDisposable
{
    public unsafe readonly Wrapped* Handle;
    public unsafe WrapperStruct(Wrapped* handle)
    {
        Handle = handle;
    }

    public bool IsEmpty
    {
        get
        {
            unsafe
            {
                return Handle == null;
            }
        }
    }

    partial void DisposeInternal();

    public void Dispose()
    {
        DisposeInternal();
        unsafe
        {
            Api.WrapedRelease(Handle);
        }
    }
}
