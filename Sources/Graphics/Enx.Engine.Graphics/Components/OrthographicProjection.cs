using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Components;

public struct OrthographicProjection
{
    public float Near;
    public float Far;
    public Vector2D<float> ViewPortOrigin;
    public float Scale;
    public Rectangle<float> Area;

    public static OrthographicProjection Default => new()
    {
        Near = -1000,
        Far = 1000,
        ViewPortOrigin = new(0.5f),
        Scale = 1,
        Area = new(new(-1), new(2))
    };
}
