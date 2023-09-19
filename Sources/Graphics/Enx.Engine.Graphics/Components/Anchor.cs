using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Sprite;

public static class Anchor
{
    public static Vector2D<float> Center => new(0, 0);
    public static Vector2D<float> BottomLeft => new(-0.5f, -0.5f);
    public static Vector2D<float> BottomCenter => new(0, -0.5f);
    public static Vector2D<float> BottomRight => new(0.5f, -0.5f);
    public static Vector2D<float> CenterLeft => new(-0.5f, 0);
    public static Vector2D<float> CenterRight => new(0.5f, 0);
    public static Vector2D<float> TopLeft => new(-0.5f, 0.5f);
    public static Vector2D<float> TopCenter => new(0, 0.5f);
    public static Vector2D<float> TopRight => new(0.5f, 0.5f);
}
