namespace Enx.WebGPU;

public struct XDeviceDescriptor
{
    public string Label { get; set; }
    public FeatureName[] RequiredFeatures { get; set; }
    public string QueueLabel { get; set; }
    public Action<DeviceLostReason, string> OnDeviceLost { get; set; }
}
