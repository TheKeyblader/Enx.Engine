using Silk.NET.Core.Native;
using Silk.NET.WebGPU.Extensions.WGPU;

namespace Enx.WebGPU;

public static class WebGPUApi
{
    private static readonly AsyncLocal<Silk.NET.WebGPU.WebGPU> _api = new();
    public static Silk.NET.WebGPU.WebGPU Api => _api.Value!;
    public static Wgpu Wgpu => new(Api.Context);

    static WebGPUApi()
    {
        _api.Value = new Silk.NET.WebGPU.WebGPU(Silk.NET.WebGPU.WebGPU.CreateDefaultContext(new string[] { "wgpu_native.dll" }));
    }

    public static LogLevel LogLevel
    {
        set
        {
            unsafe
            {
                Wgpu.SetLogLevel(value);
            }
        }
    }
    public static Action<LogLevel, string> LogCallback
    {
        set
        {
            unsafe
            {
                Wgpu.SetLogCallback(PfnLogCallback.From((LogLevel arg0, byte* arg1, void* _) =>
                {
                    var str = SilkMarshal.PtrToString((nint)arg1)!;
                    value?.Invoke(arg0, str);
                }), null);
            }
        }
    }

    public static IDisposable SetContext(Silk.NET.WebGPU.WebGPU api)
    {
        var dis = new Disposable(_api.Value!);
        _api.Value = api;
        return dis;
    }

    private readonly struct Disposable : IDisposable
    {
        private readonly Silk.NET.WebGPU.WebGPU _old;

        public Disposable(Silk.NET.WebGPU.WebGPU old)
        {
            _old = old;
        }

        public void Dispose()
        {
            _api.Value = _old;
        }
    }
}
