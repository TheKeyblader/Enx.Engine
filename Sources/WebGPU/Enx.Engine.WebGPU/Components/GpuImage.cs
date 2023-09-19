using Enx.WebGPU;
using Silk.NET.Maths;

namespace Enx.Engine.WebGPU.Components;

public class GpuImage
{
    public XTexture Texture;
    public XTextureView TextureView;
    public Vector2D<int> Size;
}
