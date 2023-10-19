namespace Enx.WebGPU;

public struct XSamplerDescriptor
{
    public XSamplerDescriptor()
    {
        LodMaxClamp = 32;
        MaxAnisotropy = 1;
    }

    public string? Label { get; set; }
    public AddressMode AddressModeU { get; set; }
    public AddressMode AddressModeV { get; set; }
    public AddressMode AddressModeW { get; set; }
    public FilterMode MagFilter { get; set; }
    public FilterMode MinFilter { get; set; }
    public MipmapFilterMode MipmapFilter { get; set; }
    public float LodMaxClamp { get; set; }
    public float LodMinClamp { get; set; }
    public CompareFunction Compare { get; set; }
    public short MaxAnisotropy { get; set; }

    public static XSamplerDescriptor Nearest => new()
    {
        MinFilter = FilterMode.Nearest,
        MagFilter = FilterMode.Nearest,
        MipmapFilter = MipmapFilterMode.Nearest,
    };

    public static XSamplerDescriptor Linear => new()
    {
        MinFilter = FilterMode.Linear,
        MagFilter = FilterMode.Linear,
        MipmapFilter = MipmapFilterMode.Linear
    };
}
