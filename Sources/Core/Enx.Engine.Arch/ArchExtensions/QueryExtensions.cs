using Arch.Core;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Arch;

public static partial class QueryExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentsIterator<T0> GetComponentsIterator<T0>(this Query query)
    {
        return new ComponentsIterator<T0>(query.GetChunkIterator());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static EntityComponentsIterator<T0> GetEntityComponentsIterator<T0>(this Query query)
    {
        return new EntityComponentsIterator<T0>(query.GetChunkIterator());
    }

    public static T0 QueryUnique<T0>(this World world)
        where T0 : struct
    {
        var desc = new QueryDescription().WithAll<T0>();
        var query = world.Query(desc);

        T0? result = null;

        foreach (var comp in query.GetComponentsIterator<T0>())
        {
            if (result.HasValue)
                throw new InvalidOperationException();
            result = comp.t0;
        }

        if (result is null) throw new InvalidOperationException();

        return result.Value;
    }
}
