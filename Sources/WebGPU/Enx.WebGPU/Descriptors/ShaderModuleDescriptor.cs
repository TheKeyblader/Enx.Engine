namespace Enx.WebGPU;

public struct XShaderModuleDescriptor
{
    public string Label { get; set; }
    public XShaderModuleCompilationHint[] Hints { get; set; }
}

public struct XShaderModuleCompilationHint
{
    public string EntryPoint { get; set; }
    public XPipelineLayout Layout { get; set; }
}

public struct XShaderModuleSPIRVDescriptor
{
    public XShaderModuleDescriptor Descriptor { get; set; }
    public string Code { get; set; }
}

public struct XShaderModuleWGSLDescriptor
{
    public XShaderModuleDescriptor Descriptor { get; set; }
    public string Code { get; set; }
}