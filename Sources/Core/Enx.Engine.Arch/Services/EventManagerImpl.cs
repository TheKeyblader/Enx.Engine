using Arch.Core;
using System.Collections;

namespace Enx.Engine.Arch.Services;

public class EventManagerImpl<T> : IEventManager<T>
{
    public EventManagerImpl(EventManagerSystem system)
    {
        system.AddManager(this);
    }


    private Queue<T> _olderEvents = new();
    private Queue<T> _newestEvents = new();

    public IEnumerable<T> NewerEvents => _newestEvents;
    public IEnumerable<T> OldestEvents => _olderEvents;

    public bool IsEmpty => Count == 0;
    public int Count => _olderEvents.Count + _newestEvents.Count;

    public void Clear()
    {
        _olderEvents.Clear();
        _newestEvents.Clear();
    }

    public IEnumerator<T> GetEnumerator()
        => new Enumerator(_olderEvents.GetEnumerator(), _newestEvents.GetEnumerator());

    public void Send(T @event) => _newestEvents.Enqueue(@event);

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public void Update()
    {
        _olderEvents.Clear();
        (_newestEvents, _olderEvents) = (_olderEvents, _newestEvents);
    }

    class Enumerator(IEnumerator<T> first, IEnumerator<T> next) : IEnumerator<T>
    {
        private readonly IEnumerator<T> _firstEnumerator = first;
        private readonly IEnumerator<T> _nextEnumerator = next;
        private IEnumerator<T> _actualEnumerator = first;

        public T Current => _actualEnumerator.Current;
        object IEnumerator.Current => Current!;

        public void Dispose()
        {
            _firstEnumerator.Dispose();
            _firstEnumerator.Dispose();
        }
        public bool MoveNext()
        {
            if (_actualEnumerator == _firstEnumerator)
            {
                if (_actualEnumerator.MoveNext()) return true;
                _actualEnumerator = _nextEnumerator;
            }

            return _actualEnumerator.MoveNext();
        }
        public void Reset()
        {
            _firstEnumerator.Reset();
            _nextEnumerator.Reset();
            _actualEnumerator = _firstEnumerator;
        }
    }
}

public class EventManagerSystem(World world) : SystemBase(world)
{
    private readonly List<IEventManager> _managers = new();

    public void AddManager(IEventManager manager)
        => _managers.Add(manager);

    public override void Update()
    {
        foreach (var manager in _managers)
            manager.Update();
    }
}
