using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Enx.Engine.Arch;

public class NativeMemoryList<T>
    where T : unmanaged
{
    internal unsafe T* _ptr;

    public ref T this[int index]
    {
        get
        {
            unsafe
            {
                return ref _ptr[index];
            }
        }
    }

    public int Count { get; private set; }
    public int Capacity { get; private set; }
    public bool IsReadOnly => false;

    public NativeMemoryList(int Capacity = 30)
    {
        unsafe
        {
            _ptr = (T*)NativeMemory.Alloc((nuint)Capacity, (nuint)sizeof(T));
        }
    }

    public void EnsureCapacity(int min)
    {
        if (min <= Count) return;

        unsafe
        {
            var newPtr = (T*)NativeMemory.Realloc(_ptr, (nuint)(sizeof(T) * min));

            if (newPtr is null)
            {
                newPtr = (T*)NativeMemory.Alloc((nuint)min, (nuint)sizeof(T));
                NativeMemory.Copy(_ptr, newPtr, (nuint)(sizeof(T) * min));
                NativeMemory.Free(_ptr);
            }

            _ptr = newPtr;
            Capacity = min;
        }
    }

    public void Add(T item)
    {
        unsafe
        {
            if (Count == Capacity) EnsureCapacity(Capacity * 2);
            this[Count] = item;
            Count++;
        }
    }

    public void Clear() => Count = 0;

    public bool Contains(T item)
    {
        for (var i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(this[i], item))
                return true;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (Count == 0) return;
        unsafe
        {
            fixed (T* ptr = array)
            {
                Buffer.MemoryCopy(_ptr, ptr + arrayIndex, array.Length * sizeof(T), Count * sizeof(T));
            }
        }
    }

    public UnsafeEnumerator<T> GetEnumerator()
    {
        unsafe
        {
            return new UnsafeEnumerator<T>(_ptr, Count);
        }
    }

    public int IndexOf(T item)
    {
        for (var i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(this[i], item))
            {
                return i;
            }
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count);
        if (Capacity == Count + 1) EnsureCapacity(Capacity + 1);

        if (index < Count)
        {
            unsafe
            {
                NativeMemory.Copy(_ptr + index, _ptr + index + 1, (nuint)(sizeof(T) * (Capacity - Count)));
            }
        }

        this[index] = item;
        Count++;
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index < 0) return false;

        RemoveAt(index);
        return true;
    }

    public void RemoveAt(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count);

        Count--;
        if (index < Count)
        {
            unsafe
            {
                NativeMemory.Copy(_ptr + index + 1, _ptr + index, (nuint)(sizeof(T) * (Count - index)));
            }
        }
    }

    public Span<T> AsSpan()
    {
        unsafe
        {
            return new Span<T>(_ptr, Count);
        }
    }

    public void Dispose()
    {
        unsafe
        {
            NativeMemory.Free(_ptr);
        }
    }
}

public ref struct UnsafeEnumerator<T>
    where T : unmanaged
{
    private unsafe readonly T* _list;
    private readonly int _count;
    private int _index;

    unsafe internal UnsafeEnumerator(T* list, int count)
    {
        _list = list;
        _count = count;
        _index = 0;
    }

    public readonly unsafe ref T Current => ref _list[_index - 1];
    public bool MoveNext()
    {
        return unchecked(_index++ < _count);
    }

    public void Reset()
    {
        _index = 0;
    }
}
