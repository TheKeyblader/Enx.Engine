namespace Enx.Engine.Arch.Groups;

[UpdateInGroup<RootSystemGroup>(OrderFirst = true)]
public class StartupSystemGroup : SystemGroup
{
    private bool _inited;
    public override bool ShouldUpdate()
    {
        if (_inited)
            return false;

        _inited = true;
        return true;
    }
}

[UpdateInGroup<StartupSystemGroup>(OrderFirst = true)]
public class PreStartupSystemGroup : SystemGroup { }

[UpdateInGroup<StartupSystemGroup>(OrderLast = true)]
public class LateStartupSystemGroup : SystemGroup { }