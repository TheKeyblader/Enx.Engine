using Enx.Engine.Arch.Groups;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Enx.Engine.Arch;

public class AutomaticSystemRunner : ISystem
{
    private readonly RootSystemGroup _rootSystem;

    public SystemGroup InternalSystem => _rootSystem;

    private readonly Type[] ExceptTypes =
    [
        typeof(AutomaticSystemRunner),
        typeof(RootSystemGroup)
    ];

    public AutomaticSystemRunner(IServiceProvider serviceProvider, IOptions<SystemRunnerConfiguration> options)
    {
        options.Value.Assemblies.Add(typeof(AutomaticSystemRunner).Assembly);

        var systems = GetSystems(options.Value.Assemblies.Distinct().ToList(), options.Value.DefaultSystemGroup ?? typeof(UpdateSystemGroup));
        _rootSystem = (RootSystemGroup)CreateSystem(typeof(RootSystemGroup), serviceProvider, systems);
    }

    static SystemGroup CreateSystem(Type systemGroupKey, IServiceProvider provider, Dictionary<Type, Type[]> systems)
    {
        var systemGroup = (SystemGroup)ActivatorUtilities.GetServiceOrCreateInstance(provider, systemGroupKey);

        if (systems.TryGetValue(systemGroupKey, out Type[]? value))
        {
            foreach (var nestedSystemType in value)
            {
                ISystem system;

                if (nestedSystemType.IsAssignableTo(typeof(SystemGroup)))
                    system = CreateSystem(nestedSystemType, provider, systems);
                else system = (ISystem)ActivatorUtilities.GetServiceOrCreateInstance(provider, nestedSystemType);

                systemGroup.AddSystem(system);
            }
        }

        return systemGroup;
    }

    Dictionary<Type, Type[]> GetSystems(List<Assembly> assemblies, Type defaultSystemGroup)
    {
        var systems = assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Contains(typeof(ISystem)))
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Except(ExceptTypes)
            .GroupBy(t => GetUpdateSystem(t) ?? defaultSystemGroup)
            .ToDictionary(g => g.Key, g => g.ToArray());

        return systems;
    }

    static Type? GetUpdateSystem(Type type)
    {
        var updateIn = type.GetCustomAttribute(typeof(UpdateInGroupAttribute<>));

        if (updateIn != null)
        {
            return updateIn.GetType().GetGenericArguments()[0];
        }

        return null;
    }

    public void Update()
    {
        _rootSystem.Update();
    }
}
