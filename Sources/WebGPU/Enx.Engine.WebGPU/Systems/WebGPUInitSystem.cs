using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Arch.Services;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU.Components;
using Enx.Engine.Window;
using Enx.WebGPU;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.WebGPU.Extensions.WGPU;
using Silk.NET.Windowing;
using System.Diagnostics;

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
            BackendType = BackendType.D3D12
        });
        var prefferedFormat = surface.GetPreferredFormat(adapter);

        var device = adapter.RequestDevice(new XDeviceDescriptor());
        device.UncapturedError = (status, message) =>
        {
            Console.WriteLine($"Device Error: {status}-{message}");
        };
        var queue = device.Queue;
        queue.WorkSubmitted = (status) => Console.WriteLine("Queue finish with status " + status);

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
            PresentMode = PresentMode.Immediate
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
    public static void Resize(Vector2D<int> size, IEventManager<FramebufferResizeEvent> events, World world, IView view)
    {
        if (size.X == 0 || size.Y == 0) return;

        events.Send(new FramebufferResizeEvent { Size = size, View = _windows[world][view] });
    }
    public static EntityReference GetEntityReference(World world, IView view)
        => _windows[world][view];
}

[UpdateInGroup<PreUpdateSystemGroup>]
public partial class WebGpuUpdateSwapchain(World world, IEventManager<FramebufferResizeEvent> events) : SystemBase<World>(world)
{
    private readonly IEventManager<FramebufferResizeEvent> _events = events;

    public override void Update()
    {
        HandleResize();
    }

    internal void HandleResize()
    {
        var device = World.QueryUnique<XDevice>();

        FramebufferResizeEvent? resizeEvent = default;
        foreach (var @event in _events)
            resizeEvent = @event;

        if (!resizeEvent.HasValue) return;

        var comp = World.Get<RenderTargetInfo, SwapChainData, SurfaceData>(resizeEvent.Value.View);
        ref var renderTargetInfo = ref comp.t0;
        renderTargetInfo.Size = resizeEvent.Value.Size;
        Debug.WriteLine(resizeEvent.Value.Size);

        if (!comp.t1.SwapChain.IsEmpty)
            comp.t1.SwapChain.Dispose();

        var descriptor = new XSwapChainDescriptor
        {
            Format = comp.t2.PreferredFormat,
            Height = resizeEvent.Value.Size.Y,
            Width = resizeEvent.Value.Size.X,
            Usage = TextureUsage.RenderAttachment,
            PresentMode = PresentMode.Immediate
        };

        ref var swapChainData = ref comp.t1;
        swapChainData.SwapChain = device.CreateSwapChain(comp.t2.Surface, descriptor);
        swapChainData.Descriptor = descriptor;
    }
}
