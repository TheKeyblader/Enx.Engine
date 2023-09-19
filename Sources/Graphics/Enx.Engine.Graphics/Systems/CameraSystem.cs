using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Graphics.Components;
using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Systems;

public partial class CameraSystem(World world) : SystemBase(world)
{
    public override void Update()
    {
        UpdateAreaQuery(World);
        UpdateOrthographicProjectionQuery(World);
    }

    [Query]
    public void UpdateArea(in Camera camera, ref OrthographicProjection orthographic)
    {
        if (!camera.RenderTarget.IsAlive()) return;

        var renderTargetInfo = World.Get<RenderTargetInfo>(camera.RenderTarget);

        var originX = renderTargetInfo.Size.X * orthographic.ViewPortOrigin.X;
        var originY = renderTargetInfo.Size.Y * orthographic.ViewPortOrigin.Y;

        orthographic.Area = new Silk.NET.Maths.Rectangle<float>
        {
            Origin = new(orthographic.Scale * -originX, orthographic.Scale * -originY),
            Size = new(orthographic.Scale * (renderTargetInfo.Size.X - originX), orthographic.Scale * (renderTargetInfo.Size.Y - originY)),
        };
    }

    [Query]
    public static void UpdateOrthographicProjection(ref Camera camera, in OrthographicProjection projection)
    {
        camera.ViewProjection = Matrix4X4.CreateOrthographicOffCenter(
            projection.Area.Origin.X,
            projection.Area.Max.X,
            projection.Area.Origin.Y,
            projection.Area.Max.Y,
            projection.Near,
            projection.Far
            );
    }
}
