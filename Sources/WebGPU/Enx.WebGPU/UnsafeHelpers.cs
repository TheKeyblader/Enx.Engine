using System.Runtime.CompilerServices;

namespace Enx.WebGPU;

public static class UnsafeHelpers
{
    public const int BufferAligment = 4;

    public static int Align(int unpadded, int alignment)
    {
        var alignMask = alignment - 1;
        var paddedSize = (unpadded + alignMask) & ~alignMask;
        return Math.Max(paddedSize, alignment);
    }

    public static int BufferAlign(int unpadded) => Align(unpadded, BufferAligment);
    public static int BufferAlign<T>() => BufferAlign(Unsafe.SizeOf<T>());
}
