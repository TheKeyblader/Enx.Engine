namespace Enx.WebGPU;

public struct XBindGroupLayoutEntry
{
    public int Binding { get; set; }
    public ShaderStage Visibility { get; set; }
    public XBufferBindingLayout? Buffer { get; set; }
    public SamplerBindingType? Sampler { get; set; }
    public XTextureBindingLayout? Texture { get; set; }
    public XStorageTextureBindingLayout? StorageTexture { get; set; }
}

public struct XBufferBindingLayout
{
    public BufferBindingType Type { get; set; }
    public bool HasDynamicOffset { get; set; }
    public long MinBindingSize { get; set; }
}

public struct XTextureBindingLayout
{
    public TextureSampleType SampleType { get; set; }
    public TextureViewDimension ViewDimension { get; set; }
    public bool Multisampled { get; set; }
}

public struct XStorageTextureBindingLayout
{
    public StorageTextureAccess Access { get; set; }
    public TextureFormat Format { get; set; }
    public TextureViewDimension ViewDimension { get; set; }
}
