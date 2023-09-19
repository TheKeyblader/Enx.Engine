using System.Reflection;

namespace Enx.Engine.Arch;

public abstract class SystemGroup : ISystem
{
    private bool _systemSortDirty = false;
    private bool _enableSystemSorting = true;

    public bool EnableSystemSorting
    {
        get => _enableSystemSorting;
        protected set
        {
            if (value && !_enableSystemSorting)
                _systemSortDirty = true;
            _enableSystemSorting = value;
        }
    }


    private readonly List<ISystem> _systemsToUpdate = new();
    private HashSet<ushort> _updateList = new();

    public virtual IReadOnlyList<ISystem> Systems => _systemsToUpdate;

    public void AddSystem(ISystem system)
    {
        _systemsToUpdate.Add(system);
        _systemSortDirty = true;
    }

    public void Update()
    {
        if (!ShouldUpdate()) return;

        if (_systemSortDirty)
            SortSystems();

        foreach (var index in _updateList)
        {
            _systemsToUpdate[index].Update();
        }
    }

    public void SortSystems()
    {
        if (EnableSystemSorting && _systemSortDirty)
        {
            SortAllSystems();
        }
        _systemSortDirty = false;

        foreach (var system in _systemsToUpdate)
        {
            if (system is SystemGroup systemGroup)
                systemGroup.SortSystems();
        }
    }

    private void SortAllSystems()
    {
        _systemsToUpdate.Sort((a, b) =>
        {
            var inA = a.GetType().GetCustomAttribute<UpdateInGroupAttribute>();
            var inB = b.GetType().GetCustomAttribute<UpdateInGroupAttribute>();

            var aValue = inA?.Value ?? 1;
            var bValue = inB?.Value ?? 1;

            return bValue - aValue;
        });

        var types = _systemsToUpdate.Select(s => s.GetType()).ToArray();

        var edges = new List<SystemEdge>();

        for (ushort i = 0; i < types.Length; i++)
        {
            var type = types[i];
            var edge = new SystemEdge { SystemIndex = i };

            var updateAfter = type.GetCustomAttribute(typeof(UpdateAfterAttribute<>));
            if (updateAfter is not null)
            {
                var systemType = updateAfter.GetType().GetGenericArguments()[0];
                if (!types.Contains(type))
                    throw new InvalidOperationException($"The System {type} can't be updated after {systemType} because they are not in same group");
                edge.PreviousIndex = (ushort)Array.IndexOf(types, systemType);
            }

            var updateBefore = type.GetCustomAttribute(typeof(UpdateBeforeAttribute<>));
            if (updateBefore is not null)
            {
                var systemType = updateBefore.GetType().GetGenericArguments()[0];
                if (!types.Contains(type))
                    throw new InvalidOperationException($"The System {type} can't be updated before {systemType} because they are not in same group");
                edge.NextIndex = (ushort)Array.IndexOf(types, systemType);
            }

            if (edge.PreviousIndex.HasValue || edge.NextIndex.HasValue)
                edges.Add(edge);
        }

        HashSet<Tuple<ushort, ushort>> construtedEdges = [];
        foreach (var edge in edges)
        {
            if (edge.NextIndex.HasValue)
                construtedEdges.Add(Tuple.Create(edge.SystemIndex, edge.NextIndex.Value));
            if (edge.PreviousIndex.HasValue)
                construtedEdges.Add(Tuple.Create(edge.PreviousIndex.Value, edge.SystemIndex));
        }

        var nodes = Enumerable.Range(0, _systemsToUpdate.Count).Select(i => (ushort)i).ToHashSet();

        var updateList = TopologicalSort(nodes, construtedEdges)
            ?? throw new InvalidOperationException($"Impossible to generate ordering for system group {GetType()}");
        _updateList = updateList;
    }

    static HashSet<T>? TopologicalSort<T>(HashSet<T> nodes, HashSet<Tuple<T, T>> edges) where T : IEquatable<T>
    {
        // Empty list that will contain the sorted elements
        var L = new HashSet<T>();

        // Set of all nodes with no incoming edges
        var S = new HashSet<T>(nodes.Where(n => edges.All(e => e.Item2.Equals(n) == false)));

        // while S is non-empty do
        while (S.Count != 0)
        {

            //  remove a node n from S
            var n = S.First();
            S.Remove(n);

            // add n to tail of L
            L.Add(n);

            // for each node m with an edge e from n to m do
            foreach (var e in edges.Where(e => e.Item1.Equals(n)).ToList())
            {
                var m = e.Item2;

                // remove edge e from the graph
                edges.Remove(e);

                // if m has no other incoming edges then
                if (edges.All(me => me.Item2.Equals(m) == false))
                {
                    // insert m into S
                    S.Add(m);
                }
            }
        }

        // if graph has edges then
        if (edges.Count != 0)
        {
            // return error (graph has at least one cycle)
            return null;
        }
        else
        {
            // return L (a topologically sorted order)
            return L;
        }
    }

    public virtual bool ShouldUpdate() => true;
}

struct SystemEdge
{
    public ushort SystemIndex;
    public ushort? PreviousIndex;
    public ushort? NextIndex;
}