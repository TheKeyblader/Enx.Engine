namespace Enx.WebGPU;

public ref struct XRenderPipelineDescriptor
{
    public string Label { get; set; }
    public XPipelineLayout? Layout { get; set; }
    public XVertexState Vertex { get; set; }
    public XPrimitiveState? Primitive { get; set; }
    public XDepthStencilState? DepthStencil { get; set; }
    public XMultisampleState? Multisample { get; set; }
    public XFragmentState? Fragment { get; set; }
}

public ref struct XVertexState
{
    public XShaderModule Module { get; set; }
    public string EntryPoint { get; set; }
    public Dictionary<string, double> Constants { get; set; }
    public XVertexBufferLayout[] Buffers { get; set; }
}

public struct XVertexBufferLayout
{
    public long ArrayStride { get; set; }
    public VertexStepMode StepMode { get; set; }
    public XVertexAttribute[] Attributes { get; set; }
}

public struct XVertexAttribute
{
    public VertexFormat Format { get; set; }
    public long Offset { get; set; }
    public int ShaderLocation { get; set; }
}
