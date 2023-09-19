namespace Enx.WebGPU;

public struct XBlendState
{
    public XBlendComponent Color { get; set; }
    public XBlendComponent Alpa { get; set; }


    public static XBlendState Replace => new()
    {
        Color = XBlendComponent.Replace,
        Alpa = XBlendComponent.Replace,
    };

    public static XBlendState AlphaBlending => new()
    {
        Color = new()
        {
            SrcFactor = BlendFactor.SrcAlpha,
            DstFactor = BlendFactor.OneMinusSrcAlpha,
            Operation = BlendOperation.Add
        },
        Alpa = XBlendComponent.Over
    };

    public static XBlendState PremultipliedAphaBlending => new()
    {
        Color = XBlendComponent.Over,
        Alpa = XBlendComponent.Over,
    };
}
