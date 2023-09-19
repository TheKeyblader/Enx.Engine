namespace Enx.Engine.Arch.Groups;

[UpdateInGroup<RootSystemGroup>]
public class UpdateSystemGroup : SystemGroup { }

[UpdateInGroup<UpdateSystemGroup>(OrderFirst = true)]
public class PreUpdateSystemGroup : SystemGroup { }

[UpdateInGroup<UpdateSystemGroup>(OrderLast = true)]
public class LateUpdateSystemGroup : SystemGroup { }

