namespace Enx.WebGPU;

public struct XCompilationMessage
{
    public string Message { get; set; }
    public CompilationMessageType Type { get; set; }
    public long LineNum { get; set; }
    public long LinePos { get; set; }
    public long Offset { get; set; }
    public long Length { get; set; }
    public long UTF16Offset { get; set; }
    public long UTF16Length { get; set; }
}
