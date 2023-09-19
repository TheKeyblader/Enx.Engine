using Silk.NET.Maths;
using System.Runtime.CompilerServices;

namespace Enx.Engine.Components;

public struct Transform
{
    public Matrix4X4<float> Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Matrix4X4<float>(Transform t) => Unsafe.As<Transform, Matrix4X4<float>>(ref t);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Transform(Matrix4X4<float> m) => Unsafe.As<Matrix4X4<float>, Transform>(ref m);
}
