using Enx.Engine.Asset;
using Enx.Engine.Graphics.Assets;
using Silk.NET.Maths;
using System.Drawing;

namespace Enx.Engine.Graphics.Components;

public struct Sprite
{
    public Handle<Image> ImageHandle;
    public Color Color;
    public bool FlipX;
    public bool FlipY;
    public Vector2D<float>? CustomSize;
    public Rectangle<float>? SourceRect;
    public Vector2D<float> Origin;

    public Sprite(Handle<Image> handle)
    {
        ImageHandle = handle;
        Color = Color.White;
        Origin = Anchor.Center;
    }
}
