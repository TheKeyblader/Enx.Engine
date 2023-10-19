using System.Reflection;

namespace Enx.Engine.Arch;

public class SystemRunnerConfiguration
{
    public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
    public Type? DefaultSystemGroup { get; set; }
}
