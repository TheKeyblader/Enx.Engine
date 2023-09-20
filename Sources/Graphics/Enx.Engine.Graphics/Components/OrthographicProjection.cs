using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Components;

public struct OrthographicProjection
{
    public float Near;
    public float Far;

    public static OrthographicProjection Default => new()
    {
        Near = -1000,
        Far = 1000,
    };
}
