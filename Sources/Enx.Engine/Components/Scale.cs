using Silk.NET.Maths;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Components;

public struct Scale
{
    public Vector3D<float> Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector3D<float>(Scale t) => Unsafe.As<Scale, Vector3D<float>>(ref t);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Scale(Vector3D<float> m) => Unsafe.As<Vector3D<float>, Scale>(ref m);
}
