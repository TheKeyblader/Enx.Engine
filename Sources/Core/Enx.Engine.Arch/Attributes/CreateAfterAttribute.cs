namespace Enx.Engine.Arch;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class CreateAfterAttribute<TSystem> : Attribute { }
