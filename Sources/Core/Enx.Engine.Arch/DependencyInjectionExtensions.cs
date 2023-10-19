using Enx.Engine.Arch.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enx.Engine.Arch;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddEcs(this IServiceCollection services)
    {
        services.AddOptions<SystemRunnerConfiguration>();
        services.AddSingleton(typeof(IEventManager<>), typeof(EventManagerImpl<>));
        services.AddSingleton(sp =>
            new AutomaticSystemRunner(sp, sp.GetRequiredService<IOptions<SystemRunnerConfiguration>>()));

        services.AddSingleton<EventManagerSystem>();

        return services;
    }
}
