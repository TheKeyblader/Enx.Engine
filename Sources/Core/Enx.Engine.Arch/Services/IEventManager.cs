namespace Enx.Engine.Arch.Services;

public interface IEventManager
{
    bool IsEmpty { get; }
    int Count { get; }
    void Clear();

    void Update();
}

public interface IEventManager<T> : IEventManager, IEnumerable<T>
{
    public IEnumerable<T> NewerEvents { get; }
    public IEnumerable<T> OldestEvents { get; }

    void Send(T @event);
}
