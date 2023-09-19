using Silk.NET.Maths;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Components;

public struct Rotation
{
    public Quaternion<float> Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Quaternion<float>(Rotation x) => Unsafe.As<Rotation, Quaternion<float>>(ref x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Rotation(Quaternion<float> x) => Unsafe.As<Quaternion<float>, Rotation>(ref x);
}
