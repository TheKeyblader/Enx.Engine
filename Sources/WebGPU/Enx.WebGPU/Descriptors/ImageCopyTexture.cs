using Silk.NET.Maths;

namespace Enx.WebGPU;

public struct XImageCopyTexture
{
    public XTexture Texture { get; set; }
    public int MipLevel { get; set; }
    public Vector3D<int> Origin { get; set; }
    public TextureAspect Aspect { get; set; }
}
