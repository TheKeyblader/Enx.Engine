using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Assets;

public abstract class Image
{
    public abstract Vector2D<int> Size { get; }
    public abstract bool IsCompressed { get; }
}
