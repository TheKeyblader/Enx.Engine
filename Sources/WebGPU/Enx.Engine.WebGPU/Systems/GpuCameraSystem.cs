using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU.Components;
using Enx.WebGPU;
using Silk.NET.Maths;
using Silk.NET.WebGPU;

namespace Enx.Engine.WebGPU.Systems;

[UpdateInGroup<PreRenderSystemGroup>]
public partial class GpuCameraSystem(World world) : SystemBase(world)
{
    private XBindGroupLayout CameraLayout;

    public override void Update()
    {
        if (CameraLayout.IsEmpty) CreateCameraLayout();
        CreateCameraBufferQuery(World);
        UpdateCameraQuery(World);
    }

    public void CreateCameraLayout()
    {
        var device = World.QueryUnique<XDevice>();

        CameraLayout = device.CreateBindGroupLayout(new XBindGroupLayoutDescriptor
        {
            Label = "Camera bind group layout",
            Entries =
            [
                new XBindGroupLayoutEntry
                {
                    Binding = 0,
                    Visibility = ShaderStage.Vertex,
                    Buffer = new XBufferBindingLayout
                    {
                        HasDynamicOffset = false,
                        Type = BufferBindingType.Uniform
                    }
                }
            ]
        });

        World.Create(new CameraLayout { Value = CameraLayout });
    }

    [Query, None(typeof(GpuCamera))]
    public void CreateCameraBuffer(in Entity entity, in Camera camera)
    {
        var device = World.QueryUnique<XDevice>();
        var buffer = device.CreateBuffer(new XBufferDescriptor
        {
            Label = "Camera buffer",
            MappedAtCreation = true,
            Usage = BufferUsage.Uniform | BufferUsage.CopyDst,
            Size = UnsafeHelpers.BufferAlign<Matrix4X4<float>>()
        });

        var bindGroup = device.CreateBindGroup(new XBindGroupDescriptor
        {
            Label = "Camera bind group",
            Layout = CameraLayout,
            Entries =
            [
                new XBindGroupEntry
                {
                    Binding = 0,
                    Buffer = buffer,
                    Size = UnsafeHelpers.BufferAlign<Matrix4X4<float>>()
                }
            ]
        });

        var span = buffer.GetMappedRange<Matrix4X4<float>>(0, 1);
        span[0] = camera.ViewProjection;
        buffer.Unmap();

        World.Add(entity, new GpuCamera
        {
            BindGroup = bindGroup,
            Buffer = buffer,
            BindGroupLayout = CameraLayout
        });
    }

    [Query]
    public void UpdateCamera(ref Camera camera, in GpuCamera gpuCamera)
    {
        var queue = World.QueryUnique<XQueue>();
        var span = new Span<Matrix4X4<float>>(ref camera.ViewProjection);
        queue.WriteBuffer(gpuCamera.Buffer, 0, span);
    }
}
