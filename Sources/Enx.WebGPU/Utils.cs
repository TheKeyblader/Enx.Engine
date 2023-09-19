namespace Enx.WebGPU;

public static class Utils
{
    public const int BufferAligment = 4;

    public static int ComputePadding(int size)
    {
        var alignMask = BufferAligment - 1;
        var paddedSize = Math.Max(((size + alignMask) & ~alignMask), BufferAligment);
        return paddedSize;
    }
}
