using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Components;
using Silk.NET.Maths;

namespace Enx.Engine.Systems;

[UpdateInGroup<LateUpdateSystemGroup>]
public partial class TransformSystem(World world) : SystemBase(world)
{
    public override void Update()
    {
        ResetTransformQuery(World);
        ApplyTranslationQuery(World);
        ApplyRotationQuery(World);
        ApplyScaleQuery(World);
    }


    [Query]
    public static void ResetTransform(ref Transform transform)
    {
        transform = Matrix4X4<float>.Identity;
    }

    [Query]
    public static void ApplyTranslation(ref Transform transform, in Position position)
    {
        transform *= Matrix4X4.CreateTranslation<float>(position);
    }

    [Query]
    public static void ApplyRotation(ref Transform transform, in Rotation rotation)
    {
        transform *= Matrix4X4.CreateFromQuaternion<float>(rotation);
    }

    [Query]
    public static void ApplyScale(ref Transform transform, in Scale scale)
    {
        transform *= Matrix4X4.CreateScale<float>(scale);
    }
}
