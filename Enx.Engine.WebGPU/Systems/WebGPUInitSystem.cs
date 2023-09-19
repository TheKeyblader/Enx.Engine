using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU.Components;
using Enx.Engine.Window;
using Enx.WebGPU;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;
using Silk.NET.Windowing;

namespace Enx.Engine.WebGPU.Systems;

public static class WebGPUSystem
{
    private static readonly Dictionary<World, Dictionary<IView, EntityReference>> _windows = new();

    public static void Initialize(IView source, World world, bool isPrimary = false)
    {
        WebGPUApi.LogLevel = LogLevel.Info;
        WebGPUApi.LogCallback = (level, message) =>
        {
            Console.WriteLine(message);
        };

        var instance = XInstance.Create();

        var surface = instance.CreateSurface(source);
        var adapter = instance.RequestAdapter(new XRequestAdapterOptions
        {
            CompatibleSurface = surface,
        });
        var prefferedFormat = surface.GetPreferredFormat(adapter);

        var device = adapter.RequestDevice(new XDeviceDescriptor { });
        device.UncapturedError = (status, message) =>
        {
            Console.WriteLine($"Device Error: {status}-{message}");
        };
        var queue = device.Queue;

        world.Create(instance, adapter, device, queue, new PreferredFormat(prefferedFormat));

        var surfaceData = new SurfaceData
        {
            Surface = surface,
            PreferredFormat = prefferedFormat
        };

        var renderTargetInfo = new RenderTargetInfo
        {
            Size = source.FramebufferSize
        };

        var descriptor = new XSwapChainDescriptor
        {
            Format = prefferedFormat,
            Height = source.FramebufferSize.Y,
            Width = source.FramebufferSize.X,
            Usage = TextureUsage.RenderAttachment,
            PresentMode = PresentMode.Fifo
        };

        var swapChainData = new SwapChainData
        {
            SwapChain = device.CreateSwapChain(surface, descriptor),
            Descriptor = descriptor
        };

        Entity windowEntity;
        if (isPrimary)
            windowEntity = world.Create(new PrimaryWindow(), surfaceData, renderTargetInfo, swapChainData);
        else windowEntity = world.Create(surfaceData, renderTargetInfo, swapChainData);

        if (!_windows.ContainsKey(world))
            _windows[world] = [];
        _windows[world][source] = world.Reference(windowEntity);
    }
    public static void Resize(Vector2D<int> size, World world, IView view)
    {
        if (size.X == 0 || size.Y == 0) return;

        world.Create(new FramebufferResizeEvent { Size = size, View = _windows[world][view] });
    }
    public static EntityReference GetEntityReference(World world, IView view)
        => _windows[world][view];
}

[UpdateInGroup<PreUpdateSystemGroup>]
public partial class WebGpuUpdateSwapchain(World world) : SystemBase<World>(world)
{
    public override void Update()
    {
        HandleResizeQuery(World);
    }

    [Query]
    internal void HandleResize(in Entity entity, in FramebufferResizeEvent resizeEvent)
    {
        var device = World.QueryUnique<XDevice>();

        var comp = World.Get<RenderTargetInfo, SwapChainData, SurfaceData>(resizeEvent.View);

        comp.t0.Size = resizeEvent.Size;

        if (!comp.t1.SwapChain.IsEmpty)
            comp.t1.SwapChain.Dispose();

        var descriptor = new XSwapChainDescriptor
        {
            Format = comp.t2.PreferredFormat,
            Height = resizeEvent.Size.Y,
            Width = resizeEvent.Size.X,
            Usage = TextureUsage.RenderAttachment,
            PresentMode = PresentMode.Fifo
        };
        comp.t1.SwapChain = device.CreateSwapChain(comp.t2.Surface, descriptor);
        comp.t1.Descriptor = descriptor;

        World.Destroy(entity);
    }
}
