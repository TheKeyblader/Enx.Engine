using Silk.NET.Maths;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Components;

public struct Position
{
    public Vector3D<float> Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector3D<float>(Position x) => Unsafe.As<Position, Vector3D<float>>(ref x);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Position(Vector3D<float> x) => Unsafe.As<Vector3D<float>, Position>(ref x);
}
