using Arch.Core;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Arch;

[SkipLocalsInit]
public readonly ref struct ComponentsIterator<T0>
{
    private readonly QueryChunkIterator _iterator;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentsIterator(QueryChunkIterator iterator)
    {
        _iterator = iterator;
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentsEnumerator<T0> GetEnumerator()
    {
        return new ComponentsEnumerator<T0>(_iterator.GetEnumerator());
    }
}

[SkipLocalsInit]
public ref struct ComponentsEnumerator<T0>
{
    private QueryChunkEnumerator _chunkEnumerator;
    private Components<T0> _first;
    private int _index;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentsEnumerator(QueryChunkEnumerator enumerator)
    {
        _chunkEnumerator = enumerator;

        if (_chunkEnumerator.MoveNext())
        {
            _first = new Components<T0>(ref _chunkEnumerator.Current.GetFirst<T0>());
            _index = _chunkEnumerator.Current.Size;
        }
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        if (--_index >= 0)
            return true;


        if (!_chunkEnumerator.MoveNext())
            return false;

        _first = new Components<T0>(ref _chunkEnumerator.Current.GetFirst<T0>());
        _index = _chunkEnumerator.Current.Size - 1;
        return true;
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _chunkEnumerator.Reset();

        if (_chunkEnumerator.MoveNext())
            _index = _chunkEnumerator.Current.Size;
    }

    public readonly Components<T0> Current
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ref var t0 = ref Unsafe.Add(ref _first.t0, _index);

            return new Components<T0>(ref t0);
        }
    }
}

[SkipLocalsInit]
public readonly ref struct EntityComponentsIterator<T0>
{
    private readonly QueryChunkIterator _iterator;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponentsIterator(QueryChunkIterator iterator)
    {
        _iterator = iterator;
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponentsEnumerator<T0> GetEnumerator()
    {
        return new EntityComponentsEnumerator<T0>(_iterator.GetEnumerator());
    }
}

[SkipLocalsInit]
public ref struct EntityComponentsEnumerator<T0>
{
    private QueryChunkEnumerator _chunkEnumerator;
    private ref Entity _firstEntity;
    private Components<T0> _first;
    private int _index;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
    {
        _chunkEnumerator = enumerator;

        if (_chunkEnumerator.MoveNext())
        {
            _firstEntity = _chunkEnumerator.Current.Entity(0);
            _first = new Components<T0>(ref _chunkEnumerator.Current.GetFirst<T0>());
            _index = _chunkEnumerator.Current.Size;
        }
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        if (--_index >= 0)
            return true;


        if (!_chunkEnumerator.MoveNext())
            return false;

        _firstEntity = _chunkEnumerator.Current.Entity(0);
        _first = new Components<T0>(ref _chunkEnumerator.Current.GetFirst<T0>());
        _index = _chunkEnumerator.Current.Size - 1;
        return true;
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _chunkEnumerator.Reset();

        if (_chunkEnumerator.MoveNext())
            _index = _chunkEnumerator.Current.Size;
    }

    public readonly EntityComponents<T0> Current
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
            ref var t0 = ref Unsafe.Add(ref _first.t0, _index);

            return new EntityComponents<T0>(ref entity, ref t0);
        }
    }
}
