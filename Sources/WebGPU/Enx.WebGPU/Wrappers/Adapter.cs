using Silk.NET.Core.Native;
using Enx.WebGPU.SourceGenerator;

namespace Enx.WebGPU;

[Wrapper(typeof(Adapter))]
public readonly partial struct XAdapter
{
    public FeatureName[] Features
    {
        get
        {
            unsafe
            {
                var size = Api.AdapterEnumerateFeatures(Handle, null);
                FeatureName[] features = new FeatureName[size];
                fixed (FeatureName* ptr = features)
                {
                    Api.AdapterEnumerateFeatures(Handle, ptr);
                }
                return features;
            }
        }
    }
    public XAdapterProperties Properties
    {
        get
        {
            unsafe
            {
                var props = new AdapterProperties();
                Api.AdapterGetProperties(Handle, ref props);
                return new XAdapterProperties(props);
            }
        }
    }

    public bool HasFeature(FeatureName feature)
    {
        unsafe
        {
            return Api.AdapterHasFeature(Handle, feature);
        }
    }
    public XDevice RequestDevice(XDeviceDescriptor descriptor)
    {
        unsafe
        {
            DeviceDescriptor deviceDescriptor = new();
            if (descriptor.Label is not null)
                deviceDescriptor.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.QueueLabel is not null)
                deviceDescriptor.DefaultQueue = new QueueDescriptor
                {
                    Label = (byte*)SilkMarshal.StringToPtr(descriptor.QueueLabel)
                };

            if (descriptor.RequiredFeatures is not null && descriptor.RequiredFeatures.Length != 0)
            {
                fixed (FeatureName* ptr = descriptor.RequiredFeatures)
                {
                    deviceDescriptor.RequiredFeatures = ptr;
                    deviceDescriptor.RequiredFeatureCount = (uint)descriptor.RequiredFeatures.Length;
                }
            }

            Device* handle = null;
            Api.AdapterRequestDevice(Handle, deviceDescriptor, PfnRequestDeviceCallback.From((RequestDeviceStatus arg0, Device* arg1, byte* arg2, void* arg3) =>
            {
                handle = arg1;
            }), null);

            return new XDevice(handle);
        }
    }
}
