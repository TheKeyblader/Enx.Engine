using CommunityToolkit.HighPerformance;

namespace Enx.Engine.Asset;

public enum HandleType : byte
{
    Weak,
    Strong
}

public readonly record struct Handle<TAsset>
{
    public readonly HandleId Id;
    public readonly HandleType Type;

    public Handle(HandleId id, HandleType type)
    {
        Id = id;
        Type = type;
    }

    public static implicit operator HandleId(Handle<TAsset> handle) => handle.Id;
}

public readonly record struct HandleId
{
    public readonly Guid Id;
    public readonly int PathId;
    public readonly int LabelId;

    public HandleId()
    {
        Id = Guid.NewGuid();
    }
    public HandleId(string path, string? label = null)
    {
        PathId = path.GetDjb2HashCode();
        LabelId = label?.GetDjb2HashCode() ?? 0;
    }
}
