using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Graphics.Components;
using Silk.NET.Maths;

namespace Enx.Engine.Graphics.Systems;

public partial class CameraSystem(World world) : SystemBase(world)
{
    public override void Update()
    {
        UpdateOrthographicProjectionQuery(World);
    }

    [Query]
    public void UpdateOrthographicProjection(ref Camera camera, in OrthographicProjection projection)
    {
        var renderTarget = World.Get<RenderTargetInfo>(camera.RenderTarget);

        camera.ViewProjection = Matrix4X4.CreateOrthographicOffCenter(0, renderTarget.Size.X, renderTarget.Size.Y, 0, projection.Near, projection.Far);
    }
}
