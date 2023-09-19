using Arch.Core;
using Silk.NET.Maths;

namespace Enx.Engine.WebGPU.Components;

internal struct FramebufferResizeEvent
{
    public Vector2D<int> Size;
    public EntityReference View;
}
