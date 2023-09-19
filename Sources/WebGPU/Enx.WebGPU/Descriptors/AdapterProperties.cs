using Silk.NET.Core.Native;

namespace Enx.WebGPU;

public struct XAdapterProperties
{

    public unsafe XAdapterProperties(AdapterProperties properties)
    {
        VendorID = (int)properties.VendorID;
        VendorName = SilkMarshal.PtrToString((nint)properties.VendorName);
        Architecture = SilkMarshal.PtrToString((nint)properties.Architecture);
        DeviceId = (int)properties.DeviceID;
        Name = SilkMarshal.PtrToString((nint)properties.Name);
        DriverDescription = SilkMarshal.PtrToString((nint)properties.DriverDescription);
        AdapterType = properties.AdapterType;
        BackendType = properties.BackendType;
    }

    public int VendorID { get; set; }
    public string? VendorName { get; set; }
    public string? Architecture { get; set; }
    public int DeviceId { get; set; }
    public string? Name { get; set; }
    public string? DriverDescription { get; set; }
    public AdapterType AdapterType { get; set; }
    public BackendType BackendType { get; set; }
}
