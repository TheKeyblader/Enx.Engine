using Enx.WebGPU;

namespace Enx.Engine.WebGPU;

public struct SwapChainData
{
    public XSwapChain SwapChain;
    public XTextureView SwapChainView;
    public XSwapChainDescriptor Descriptor;
}
