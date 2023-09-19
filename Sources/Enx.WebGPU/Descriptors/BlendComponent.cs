namespace Enx.WebGPU;

public struct XBlendComponent
{
    public BlendOperation Operation { get; set; }
    public BlendFactor SrcFactor { get; set; }
    public BlendFactor DstFactor { get; set; }

    public BlendComponent ToNative()
    {
        return new BlendComponent
        {
            DstFactor = DstFactor,
            Operation = Operation,
            SrcFactor = SrcFactor,
        };
    }

    public static XBlendComponent Replace => new()
    {
        SrcFactor = BlendFactor.One,
        DstFactor = BlendFactor.Zero,
        Operation = BlendOperation.Add
    };

    public static XBlendComponent Over => new()
    {
        SrcFactor = BlendFactor.One,
        DstFactor = BlendFactor.OneMinusSrcAlpha,
        Operation = BlendOperation.Add
    };
}
