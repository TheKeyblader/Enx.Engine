using Silk.NET.WebGPU;

namespace Enx.Engine.WebGPU.Components;

public struct PreferredFormat(TextureFormat value)
{
    public TextureFormat Value = value;

    public static implicit operator TextureFormat(PreferredFormat value) => value.Value;
    public static implicit operator PreferredFormat(TextureFormat texture) => new(texture);
}
