using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(SwapChain))]
public readonly partial struct XSwapChain
{
    public XTextureView GetCurrentTextureView()
    {
        unsafe
        {
            var handle = Api.SwapChainGetCurrentTextureView(Handle);
            return new XTextureView(handle);
        }
    }
    public void Present()
    {
        unsafe
        {
            Api.SwapChainPresent(Handle);
        }
    }
}
