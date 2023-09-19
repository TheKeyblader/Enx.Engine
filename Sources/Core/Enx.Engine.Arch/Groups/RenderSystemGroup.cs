namespace Enx.Engine.Arch.Groups;

[UpdateInGroup<RootSystemGroup>(OrderLast = true)]
public class RenderSystemGroup : SystemGroup { }

[UpdateInGroup<RenderSystemGroup>(OrderFirst = true)]
public class PreRenderSystemGroup : SystemGroup { }

[UpdateInGroup<RenderSystemGroup>(OrderLast = true)]
public class LateRenderSystemGroup : SystemGroup { }