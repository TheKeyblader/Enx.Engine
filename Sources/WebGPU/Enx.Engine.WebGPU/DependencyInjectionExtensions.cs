using Enx.Engine.Asset;
using Enx.Engine.Graphics.Assets;
using Enx.Engine.WebGPU.Assets;
using Enx.Engine.WebGPU.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Enx.Engine.WebGPU;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWebGPU(this IServiceCollection services)
    {
        services.AddSingleton<IAssetLoader<Image>, ImageLoader>();
        services.AddSingleton<IAssetTransformer<Image, GpuImage>, ImageTransformer>();
        return services;
    }
}
