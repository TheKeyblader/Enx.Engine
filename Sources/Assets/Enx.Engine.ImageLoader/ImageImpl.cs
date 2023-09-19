using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Assets;

public class ImageImpl : Image
{
    public override Vector2D<int> Size { get; }
    public override bool IsCompressed { get; }
    public byte[] Data { get; }

    public ImageImpl(Vector2D<int> size, bool isCompressed, byte[] data)
    {
        Size = size;
        IsCompressed = isCompressed;
        Data = data;
    }
}
