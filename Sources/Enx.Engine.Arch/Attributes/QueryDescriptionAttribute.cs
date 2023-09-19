namespace Enx.Engine.Arch;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class AllAttribute(params Type[] componentTypes) : Attribute
{
    public Type[] ComponentTypes { get; set; } = componentTypes;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class AnyAttribute(params Type[] componentTypes) : Attribute
{
    public Type[] ComponentTypes { get; set; } = componentTypes;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class NoneAttribute(params Type[] componentTypes) : Attribute
{
    public Type[] ComponentTypes { get; set; } = componentTypes;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class ExclusiveAttribute(params Type[] componentTypes) : Attribute
{
    public Type[] ComponentTypes { get; set; } = componentTypes;
}
