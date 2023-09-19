using Arch.Core;
using System.Runtime.CompilerServices;
namespace Enx.Engine.Arch {

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1>
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
        public ComponentsEnumerator<T0,T1> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2>
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
        public ComponentsEnumerator<T0,T1,T2> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3>
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
        public ComponentsEnumerator<T0,T1,T2,T3> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>(_iterator.GetEnumerator());
        }
    }

    [SkipLocalsInit]
    public readonly ref struct ComponentsIterator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>
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
        public ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24> GetEnumerator()
        {
            return new ComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>(_iterator.GetEnumerator());
        }
    }
}
