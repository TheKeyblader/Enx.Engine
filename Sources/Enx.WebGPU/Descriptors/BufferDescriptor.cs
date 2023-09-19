namespace Enx.WebGPU;

public struct XBufferDescriptor
{
    public string Label { get; set; }
    public BufferUsage Usage { get; set; }
    public long Size { get; set; }
    public bool MappedAtCreation { get; set; }
}
