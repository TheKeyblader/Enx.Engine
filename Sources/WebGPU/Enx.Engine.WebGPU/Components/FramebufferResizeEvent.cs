using Arch.Core;
using Silk.NET.Maths;

namespace Enx.Engine.WebGPU.Components;

public struct FramebufferResizeEvent
{
    public Vector2D<int> Size;
    public EntityReference View;
}
