namespace Enx.WebGPU;

public struct XRequestAdapterOptions
{
    public XSurface? CompatibleSurface { get; set; }
    public PowerPreference PowerPreference { get; set; }
    public BackendType BackendType { get; set; }
    public bool ForceFallbackAdapter { get; set; }
}
