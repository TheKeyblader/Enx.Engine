using Enx.WebGPU.SourceGenerator;
using System.Runtime.CompilerServices;
using Buffer = Silk.NET.WebGPU.Buffer;

namespace Enx.WebGPU;

[Wrapper(typeof(Buffer))]
public readonly partial struct XBuffer
{
    //public BufferMapState MapState
    //{
    //    get
    //    {
    //        unsafe
    //        {
    //            return Api.BufferGetMapState(Handle);
    //        }
    //    }
    //}
    public long Size
    {
        get
        {
            unsafe
            {
                return (long)Api.BufferGetSize(Handle);
            }
        }
    }
    public BufferUsage Usage
    {
        get
        {
            unsafe
            {
                return Api.BufferGetUsage(Handle);
            }
        }
    }
    public string Label
    {
        set
        {
            unsafe
            {
                Api.BufferSetLabel(Handle, value);
            }
        }
    }

    public void Map(MapMode mode, int offset, int size)
    {
        unsafe
        {
            Api.BufferMapAsync(Handle, mode, (uint)offset, (uint)size, PfnBufferMapCallback.From((BufferMapAsyncStatus arg0, void* _) =>
            {
                if (arg0 != BufferMapAsyncStatus.Success) throw new Exception();
            }), null);
        }
    }
    public Span<T> GetMappedRange<T>(int offset, int length)
    {
        unsafe
        {
            var size = Unsafe.SizeOf<T>();
            if (Usage.HasFlag(BufferUsage.Vertex)) size = UnsafeHelpers.BufferAlign<T>();
            var data = Api.BufferGetMappedRange(Handle, (uint)(offset * size), (uint)(size * length));
            var span = new Span<T>(data, length);
            return span;
        }
    }
    public ReadOnlySpan<T> GetConstMappedRange<T>(int offset, int length)
    {
        unsafe
        {
            var size = Utils.ComputePadding(Unsafe.SizeOf<T>());
            var data = Api.BufferGetConstMappedRange(Handle, (uint)offset, (uint)(size * length));
            var span = new ReadOnlySpan<T>(data, length);
            return span;
        }
    }
    public void Unmap()
    {
        unsafe
        {
            Api.BufferUnmap(Handle);
        }
    }

    partial void DisposeInternal()
    {
        unsafe
        {
            Api.BufferDestroy(Handle);
        }
    }
}
