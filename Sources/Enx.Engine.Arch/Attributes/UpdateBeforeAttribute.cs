namespace Enx.Engine.Arch;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class UpdateBeforeAttribute<TSystem> : Attribute { }