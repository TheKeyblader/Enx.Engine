

using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Arch.Services;
using Enx.Engine.Asset;
using Enx.Engine.Components;
using Enx.Engine.Graphics.Assets;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU;
using Enx.Engine.WebGPU.Components;
using Enx.Engine.WebGPU.Systems;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Drawing;

var options = WindowOptions.Default;
options.API = GraphicsAPI.None;
options.IsContextControlDisabled = true;
options.ShouldSwapAutomatically = false;

var window = Window.Create(options);

var world = World.Create();
var services = new ServiceCollection();

services.AddEcs().AddWebGPU();
services.AddSingleton(world);
services.AddSingleton<IAssetManager, AssetManagerImpl>();
services.AddSingleton<IView>(window);

services.Configure<SystemRunnerConfiguration>(opt =>
{
    opt.Assemblies.Add(typeof(WebGPUSystem).Assembly);
    opt.Assemblies.Add(typeof(Position).Assembly);
    opt.Assemblies.Add(typeof(Camera).Assembly);
    opt.Assemblies.Add(typeof(TestSystem).Assembly);
});

var provider = services.BuildServiceProvider();

var systems = provider.GetRequiredService<AutomaticSystemRunner>();

window.Size = new(1280, 720);
window.Load += Load;
window.Resize += Resize;
window.Render += Render;

window.Run();

void Load()
{
    WebGPUSystem.Initialize(window, world, true);
}

void Render(double delta)
{
    systems.Update();
}

void Resize(Vector2D<int> size)
{
    WebGPUSystem.Resize(size, provider.GetService<IEventManager<FramebufferResizeEvent>>()!, world, window);
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

        var image = assetManager.Load<Image>("./Image.png");
        world.Reserve([typeof(Sprite), typeof(Transform), typeof(Position)], 128 * 72);

        //world.Create(new Sprite(image)
        //{
        //    CustomSize = new(20, 20),
        //    Origin = Anchor.BottomLeft,
        //    Color = Color.Red
        //}, new Transform(), new Position()
        //{
        //    Value = new (0,0,1)
        //});

        var rand = new Random();
        for (var x = 0; x < 128; x++)
        {
            for (var y = 0; y < 72; y++)
            {
                world.Create(new Sprite(image)
                {
                    CustomSize = new(10, 10),
                    Origin = Anchor.BottomLeft,
                    Color = Color.FromArgb(255, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)),
                }, new Transform(), new Position()
                {
                    Value = new(x * 10, y * 10, 0)
                });
            }
        }
    }
}