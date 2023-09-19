namespace Enx.WebGPU;

public struct XTextureDataLayout
{
    public long Offset { get; set; }
    public int BytesPerRow { get; set; }
    public int RowsPerImage { get; set; }
}
