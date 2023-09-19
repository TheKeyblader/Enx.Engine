using Arch.Core;
using System.Runtime.CompilerServices;
namespace Enx.Engine.Arch {

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1>();
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

        public readonly EntityComponents<T0,T1> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);

                return new EntityComponents<T0,T1>(ref entity, ref t0, ref t1);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2>();
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

        public readonly EntityComponents<T0,T1,T2> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);

                return new EntityComponents<T0,T1,T2>(ref entity, ref t0, ref t1, ref t2);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3>();
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

        public readonly EntityComponents<T0,T1,T2,T3> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);

                return new EntityComponents<T0,T1,T2,T3>(ref entity, ref t0, ref t1, ref t2, ref t3);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);

                return new EntityComponents<T0,T1,T2,T3,T4>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);
                ref var t20 = ref Unsafe.Add(ref _first.t20, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19, ref t20);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);
                ref var t20 = ref Unsafe.Add(ref _first.t20, _index);
                ref var t21 = ref Unsafe.Add(ref _first.t21, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19, ref t20, ref t21);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);
                ref var t20 = ref Unsafe.Add(ref _first.t20, _index);
                ref var t21 = ref Unsafe.Add(ref _first.t21, _index);
                ref var t22 = ref Unsafe.Add(ref _first.t22, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19, ref t20, ref t21, ref t22);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);
                ref var t20 = ref Unsafe.Add(ref _first.t20, _index);
                ref var t21 = ref Unsafe.Add(ref _first.t21, _index);
                ref var t22 = ref Unsafe.Add(ref _first.t22, _index);
                ref var t23 = ref Unsafe.Add(ref _first.t23, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19, ref t20, ref t21, ref t22, ref t23);
            }
        }
    }

    [SkipLocalsInit]
    public ref struct EntityComponentsEnumerator<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>
    {
        private QueryChunkEnumerator _chunkEnumerator;
        private ref Entity _firstEntity;
        private Components<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24> _first;
        private int _index;

        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityComponentsEnumerator(QueryChunkEnumerator enumerator)
        {
            _chunkEnumerator = enumerator;

            if (_chunkEnumerator.MoveNext())
            {
                _firstEntity = _chunkEnumerator.Current.Entity(0);
                _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>();
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
            _first = _chunkEnumerator.Current.GetFirst<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>();
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

        public readonly EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24> Current
        {
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ref var entity = ref Unsafe.Add(ref _firstEntity, _index);
                ref var t0 = ref Unsafe.Add(ref _first.t0, _index);
                ref var t1 = ref Unsafe.Add(ref _first.t1, _index);
                ref var t2 = ref Unsafe.Add(ref _first.t2, _index);
                ref var t3 = ref Unsafe.Add(ref _first.t3, _index);
                ref var t4 = ref Unsafe.Add(ref _first.t4, _index);
                ref var t5 = ref Unsafe.Add(ref _first.t5, _index);
                ref var t6 = ref Unsafe.Add(ref _first.t6, _index);
                ref var t7 = ref Unsafe.Add(ref _first.t7, _index);
                ref var t8 = ref Unsafe.Add(ref _first.t8, _index);
                ref var t9 = ref Unsafe.Add(ref _first.t9, _index);
                ref var t10 = ref Unsafe.Add(ref _first.t10, _index);
                ref var t11 = ref Unsafe.Add(ref _first.t11, _index);
                ref var t12 = ref Unsafe.Add(ref _first.t12, _index);
                ref var t13 = ref Unsafe.Add(ref _first.t13, _index);
                ref var t14 = ref Unsafe.Add(ref _first.t14, _index);
                ref var t15 = ref Unsafe.Add(ref _first.t15, _index);
                ref var t16 = ref Unsafe.Add(ref _first.t16, _index);
                ref var t17 = ref Unsafe.Add(ref _first.t17, _index);
                ref var t18 = ref Unsafe.Add(ref _first.t18, _index);
                ref var t19 = ref Unsafe.Add(ref _first.t19, _index);
                ref var t20 = ref Unsafe.Add(ref _first.t20, _index);
                ref var t21 = ref Unsafe.Add(ref _first.t21, _index);
                ref var t22 = ref Unsafe.Add(ref _first.t22, _index);
                ref var t23 = ref Unsafe.Add(ref _first.t23, _index);
                ref var t24 = ref Unsafe.Add(ref _first.t24, _index);

                return new EntityComponents<T0,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24>(ref entity, ref t0, ref t1, ref t2, ref t3, ref t4, ref t5, ref t6, ref t7, ref t8, ref t9, ref t10, ref t11, ref t12, ref t13, ref t14, ref t15, ref t16, ref t17, ref t18, ref t19, ref t20, ref t21, ref t22, ref t23, ref t24);
            }
        }
    }
}
