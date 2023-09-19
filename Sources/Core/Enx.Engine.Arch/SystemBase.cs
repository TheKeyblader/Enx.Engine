using Arch.Core;

namespace Enx.Engine.Arch;

public abstract class SystemBase<TWorld> : ISystem
    where TWorld : World
{
    public TWorld World { get; private set; }

    protected SystemBase(TWorld world)
    {
        World = world;
    }

    public abstract void Update();
}

public abstract class SystemBase : SystemBase<World>
{
    protected SystemBase(World world) : base(world)
    {
    }
}
