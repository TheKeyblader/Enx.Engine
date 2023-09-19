using Enx.WebGPU;

namespace Enx.Engine.WebGPU.Components;

public struct GpuCamera
{
    public XBindGroup BindGroup;
    public XBindGroupLayout BindGroupLayout;
    public XBuffer Buffer;
}

public struct CameraLayout
{
    public XBindGroupLayout Value;
}
