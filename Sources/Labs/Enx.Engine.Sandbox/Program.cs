

using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Asset;
using Enx.Engine.Components;
using Enx.Engine.Graphics.Assets;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU.Assets;
using Enx.Engine.WebGPU.Components;
using Enx.Engine.WebGPU.Systems;
using Enx.WebGPU;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Maths;
using Silk.NET.WebGPU.Extensions.WGPU;
using Silk.NET.Windowing;
using System.Drawing;

var options = WindowOptions.Default;
options.API = GraphicsAPI.None;
options.IsContextControlDisabled = true;
options.ShouldSwapAutomatically = false;

var window = Window.Create(options);

var world = World.Create();
var services = new ServiceCollection();

services.AddSingleton(world);
services.AddSingleton<IAssetManager, AssetManagerImpl>();
services.AddSingleton<IAssetLoader<Image>, ImageLoader>();
services.AddSingleton<IAssetTransformer<Image, GpuImage>, ImageTransformer>();
services.AddSingleton<IView>(window);

var provider = services.BuildServiceProvider();

var systems = new AutomaticSystemRunner(
    provider,
    typeof(WebGPUSystem).Assembly,
    typeof(Position).Assembly,
    typeof(Camera).Assembly,
    typeof(TestSystem).Assembly);

window.Load += Load;
window.Resize += Resize;
window.Render += Render;

window.Run();

void Load()
{
    WebGPUApi.LogLevel = LogLevel.Info;
    WebGPUApi.LogCallback = (level, message) =>
    {
        Console.WriteLine($"{level}-{message}");
    };

    WebGPUSystem.Initialize(window, world, true);
}

void Render(double delta)
{
    systems.Update();
}

void Resize(Vector2D<int> size)
{
    WebGPUSystem.Resize(size, world, window);
}


static partial class TestSystem
{
    [Query, System]
    [UpdateInGroup<StartupSystemGroup>]
    public static void InitScene(World world, IView view, IAssetManager assetManager)
    {
        world.Create(new Camera
        {
            RenderTarget = WebGPUSystem.GetEntityReference(world, view),
        }, OrthographicProjection.Default, new Transform());

        world.Create(new Transform(), new Sprite(assetManager.Load<Image>("./Image.png"))
        {
            Color = Color.Red
        }, new Position() { Value = new Vector3D<float>(450, 450, 0) });
    }
}