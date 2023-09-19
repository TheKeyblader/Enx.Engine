using Arch.Core;
using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Components;

public struct Camera
{
    public EntityReference RenderTarget;
    public Matrix4X4<float> ViewProjection;
}
