using Silk.NET.Maths;
using System.Drawing;

namespace Enx.Engine.WebGPU.Helpers;

public static class ColorExtensions
{
    public static Vector4D<float> ToLinearVector(this Color color)
    {
        return new(
            (color.R / 255f).ToLinearSRGB(),
            (color.G / 255f).ToLinearSRGB(),
            (color.B / 255f).ToLinearSRGB(),
            (color.A / 255f).ToLinearSRGB());
    }

    public static float ToLinearSRGB(this float value)
    {
        if (value < 0) return value;

        if (value < 0.04045f) return value / 12.92f;
        else return MathF.Pow((value + 0.055f) / 1.055f, 2.4f);
    }
}
