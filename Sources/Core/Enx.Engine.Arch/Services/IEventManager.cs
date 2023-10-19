namespace Enx.Engine.Arch.Services;

public interface IEventManager<T> : IEnumerable<T>
{
    public IEnumerable<T> NewerEvents { get; }
    public IEnumerable<T> OldestEvents { get; }

    void Send(T @event);
    void SendDefault();
    void SendBatch();


    bool IsEmpty { get; }
    int Count { get; }
    void Clear();
}
