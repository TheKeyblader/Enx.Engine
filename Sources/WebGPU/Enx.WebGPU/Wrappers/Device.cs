using Enx.WebGPU.SourceGenerator;
using Silk.NET.Core.Native;

namespace Enx.WebGPU;

[Wrapper(typeof(Device))]
public readonly partial struct XDevice
{
    public Action<ErrorType, string> UncapturedError
    {
        set
        {
            unsafe
            {
                Api.DeviceSetUncapturedErrorCallback(Handle, PfnErrorCallback.From((ErrorType type, byte* message, void* _) =>
                {
                    var str = SilkMarshal.PtrToString((nint)message);
                    value(type, str!);
                }), null);
            }
        }
    }
    public XQueue Queue
    {
        get
        {
            unsafe
            {
                var handle = Api.DeviceGetQueue(Handle);
                return new XQueue(handle);
            }
        }
    }


    public XShaderModule CreateWGSLShader(XShaderModuleWGSLDescriptor descriptor)
    {
        unsafe
        {
            var wgsl = new ShaderModuleWGSLDescriptor
            {
                Code = (byte*)SilkMarshal.StringToPtr(descriptor.Code),
                Chain = new ChainedStruct(null, SType.ShaderModuleWgslDescriptor)
            };

            var shader = new ShaderModuleDescriptor((ChainedStruct*)&wgsl);

            if (descriptor.Descriptor.Label is not null)
                shader.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Descriptor.Label);

            if (descriptor.Descriptor.Hints is not null && descriptor.Descriptor.Hints.Length != 0)
            {
                var hints = descriptor.Descriptor.Hints.Select(h => new ShaderModuleCompilationHint
                {
                    EntryPoint = (byte*)SilkMarshal.StringToPtr(h.EntryPoint),
                    Layout = h.Layout.Handle,
                });
            }

            var handle = Api.DeviceCreateShaderModule(Handle, shader);
            return new XShaderModule(handle);
        }
    }
    public XBindGroupLayout CreateBindGroupLayout(XBindGroupLayoutDescriptor descriptor)
    {
        unsafe
        {
            var bindGroupLayout = new BindGroupLayoutDescriptor();

            if (!string.IsNullOrEmpty(descriptor.Label))
                bindGroupLayout.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.Entries != null && descriptor.Entries.Length != 0)
            {
                BindGroupLayoutEntry* entries = stackalloc BindGroupLayoutEntry[descriptor.Entries.Length];
                for (int i = 0; i < descriptor.Entries.Length; i++)
                {
                    var descriptorEntry = descriptor.Entries[i];
                    var entry = new BindGroupLayoutEntry
                    {
                        Visibility = descriptorEntry.Visibility,
                        Binding = (uint)descriptorEntry.Binding,
                    };

                    if (descriptorEntry.Buffer.HasValue)
                    {
                        var buffer = descriptorEntry.Buffer.Value;
                        entry.Buffer = new BufferBindingLayout
                        {
                            HasDynamicOffset = buffer.HasDynamicOffset,
                            MinBindingSize = (uint)buffer.MinBindingSize,
                            Type = buffer.Type,
                        };
                    }

                    if (descriptorEntry.Sampler.HasValue)
                        entry.Sampler = new SamplerBindingLayout(null, descriptorEntry.Sampler.Value);

                    if (descriptorEntry.Texture.HasValue)
                    {
                        var texture = descriptorEntry.Texture.Value;
                        entry.Texture = new TextureBindingLayout
                        {
                            Multisampled = texture.Multisampled,
                            SampleType = texture.SampleType,
                            ViewDimension = texture.ViewDimension,
                        };
                    }

                    if (descriptorEntry.StorageTexture.HasValue)
                    {
                        var storageTexture = descriptorEntry.StorageTexture.Value;
                        entry.StorageTexture = new StorageTextureBindingLayout
                        {
                            Access = storageTexture.Access,
                            Format = storageTexture.Format,
                            ViewDimension = storageTexture.ViewDimension,
                        };
                    }

                    entries[i] = entry;
                }

                bindGroupLayout.EntryCount = (uint)descriptor.Entries.Length;
                bindGroupLayout.Entries = entries;
            }

            var handle = Api.DeviceCreateBindGroupLayout(Handle, bindGroupLayout);
            return new XBindGroupLayout(handle);
        }
    }
    public XPipelineLayout CreatePipelineLayout(XPipelineLayoutDescriptor descriptor)
    {
        unsafe
        {
            var pipeline = new PipelineLayoutDescriptor();

            if (!string.IsNullOrEmpty(descriptor.Label))
                pipeline.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.BindGroupLayouts != null && descriptor.BindGroupLayouts.Length != 0)
            {
                BindGroupLayout** layouts = stackalloc BindGroupLayout*[descriptor.BindGroupLayouts.Length];
                for (int i = 0; i < descriptor.BindGroupLayouts.Length; i++)
                {
                    layouts[i] = descriptor.BindGroupLayouts[i].Handle;
                }

                pipeline.BindGroupLayouts = layouts;
                pipeline.BindGroupLayoutCount = (uint)descriptor.BindGroupLayouts.Length;
            }

            var handle = Api.DeviceCreatePipelineLayout(Handle, pipeline);
            return new XPipelineLayout(handle);
        }
    }
    public XRenderPipeline CreateRenderPipeline(XRenderPipelineDescriptor descriptor)
    {
        unsafe
        {
            var renderPipeline = new RenderPipelineDescriptor();

            {
                renderPipeline.Vertex = new VertexState
                {
                    EntryPoint = (byte*)SilkMarshal.StringToPtr(descriptor.Vertex.EntryPoint),
                    Module = descriptor.Vertex.Module.Handle,
                };

                if (descriptor.Vertex.Buffers is not null && descriptor.Vertex.Buffers.Length != 0)
                {
                    var buffers = descriptor.Vertex.Buffers.Select(b =>
                    {
                        var bufferlayout = new VertexBufferLayout
                        {
                            ArrayStride = (ulong)b.ArrayStride,
                            StepMode = b.StepMode,

                        };

                        if (b.Attributes is not null && b.Attributes.Length != 0)
                        {
                            var attributes = b.Attributes.Select(a => new VertexAttribute
                            {
                                Format = a.Format,
                                Offset = (ulong)a.Offset,
                                ShaderLocation = (uint)a.ShaderLocation
                            }).ToArray();

                            fixed (VertexAttribute* ptr = attributes)
                            {
                                bufferlayout.Attributes = ptr;
                                bufferlayout.AttributeCount = (uint)attributes.Length;
                            }
                        }

                        return bufferlayout;
                    }).ToArray();

                    fixed (VertexBufferLayout* ptr = buffers)
                    {
                        renderPipeline.Vertex.Buffers = ptr;
                        renderPipeline.Vertex.BufferCount = (uint)buffers.Length;
                    }
                }

                if (descriptor.Vertex.Constants is not null && descriptor.Vertex.Constants.Count != 0)
                {
                    var array = descriptor.Vertex.Constants.ToArray();
                    var constants = new ConstantEntry[descriptor.Vertex.Constants.Count];
                    for (int i = 0; i < constants.Length; i++)
                    {
                        var entry = array[i];
                        constants[i] = new ConstantEntry
                        {
                            Key = (byte*)SilkMarshal.StringToPtr(entry.Key),
                            Value = entry.Value
                        };
                    }

                    fixed (ConstantEntry* ptr = constants)
                    {
                        renderPipeline.Vertex.Constants = ptr;
                        renderPipeline.Vertex.ConstantCount = (uint)constants.Length;
                    }
                }
            }

            if (descriptor.Label is not null)
                renderPipeline.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.Layout.HasValue)
                renderPipeline.Layout = descriptor.Layout.Value.Handle;

            if (descriptor.Primitive.HasValue)
            {
                renderPipeline.Primitive = new PrimitiveState
                {
                    CullMode = descriptor.Primitive.Value.CullMode,
                    FrontFace = descriptor.Primitive.Value.FrontFace,
                    StripIndexFormat = descriptor.Primitive.Value.StripIndexFormat,
                    Topology = descriptor.Primitive.Value.Topology,
                };
            }

            if (descriptor.DepthStencil.HasValue)
            {
                var depth = new DepthStencilState
                {
                    DepthBias = descriptor.DepthStencil.Value.DepthBias,
                    DepthBiasClamp = descriptor.DepthStencil.Value.DepthBiasClamp,
                    DepthBiasSlopeScale = descriptor.DepthStencil.Value.DepthBiasSlopeScale,
                    DepthCompare = descriptor.DepthStencil.Value.DepthCompare,
                    DepthWriteEnabled = descriptor.DepthStencil.Value.DepthWriteEnabled,
                    Format = descriptor.DepthStencil.Value.Format,
                    StencilBack = descriptor.DepthStencil.Value.StencilBack.ToNative(),
                    StencilFront = descriptor.DepthStencil.Value.StencilFront.ToNative(),
                    StencilReadMask = (uint)descriptor.DepthStencil.Value.StencilReadMask,
                    StencilWriteMask = (uint)descriptor.DepthStencil.Value.StencilWriteMask
                };
                renderPipeline.DepthStencil = &depth;
            }

            if (descriptor.Multisample.HasValue)
            {
                renderPipeline.Multisample = new MultisampleState
                {
                    AlphaToCoverageEnabled = descriptor.Multisample.Value.AlphaToCoverageEnabled,
                    Count = (uint)descriptor.Multisample.Value.Count,
                    Mask = descriptor.Multisample.Value.Mask,
                };
            }

            if (descriptor.Fragment.HasValue)
            {
                var frag = descriptor.Fragment.Value;
                var fragment = new FragmentState
                {
                    EntryPoint = (byte*)SilkMarshal.StringToPtr(frag.EntryPoint),
                    Module = frag.Module.Handle
                };

                if (frag.Targets is not null && frag.Targets.Length != 0)
                {
                    var targets = new ColorTargetState[frag.Targets.Length];
                    var blends = new BlendState[frag.Targets.Length];
                    for (int i = 0; i < frag.Targets.Length; i++)
                    {
                        blends[i] = new BlendState
                        {
                            Alpha = frag.Targets[i].Blend.Alpa.ToNative(),
                            Color = frag.Targets[i].Blend.Color.ToNative(),
                        };

                        fixed (BlendState* blend = &blends[i])
                            targets[i] = new ColorTargetState
                            {
                                Blend = blend,
                                Format = frag.Targets[i].Format,
                                WriteMask = frag.Targets[i].WriteMask
                            };
                    }

                    fixed (ColorTargetState* ptr = targets)
                    {
                        fragment.Targets = ptr;
                        fragment.TargetCount = (uint)targets.Length;
                    }
                }

                if (frag.Constants is not null && frag.Constants.Count != 0)
                {
                    var array = frag.Constants.ToArray();
                    var constants = new ConstantEntry[frag.Constants.Count];
                    for (int i = 0; i < constants.Length; i++)
                    {
                        var entry = array[i];
                        constants[i] = new ConstantEntry
                        {
                            Key = (byte*)SilkMarshal.StringToPtr(entry.Key),
                            Value = entry.Value
                        };
                    }

                    fixed (ConstantEntry* ptr = constants)
                    {
                        fragment.Constants = ptr;
                        fragment.ConstantCount = (uint)constants.Length;
                    }
                }

                renderPipeline.Fragment = &fragment;
            }

            var handle = Api.DeviceCreateRenderPipeline(Handle, renderPipeline);
            return new XRenderPipeline(handle);
        }
    }
    public XBindGroup CreateBindGroup(XBindGroupDescriptor descriptor)
    {
        unsafe
        {
            var bindGroup = new BindGroupDescriptor()
            {
                Layout = descriptor.Layout.Handle
            };

            if (!string.IsNullOrEmpty(descriptor.Label))
                bindGroup.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.Entries != null && descriptor.Entries.Length != 0)
            {
                BindGroupEntry* entries = stackalloc BindGroupEntry[descriptor.Entries.Length];
                for (var i = 0; i < descriptor.Entries.Length; i++)
                {
                    var descriptorEntry = descriptor.Entries[i];
                    var entry = new BindGroupEntry
                    {
                        Binding = (uint)descriptorEntry.Binding,
                        Offset = (ulong)descriptorEntry.Offset,
                        Size = (ulong)descriptorEntry.Size,
                    };

                    if (descriptorEntry.Buffer.HasValue)
                        entry.Buffer = descriptorEntry.Buffer.Value.Handle;
                    if (descriptorEntry.Sampler.HasValue)
                        entry.Sampler = descriptorEntry.Sampler.Value.Handle;
                    if (descriptorEntry.TextureView.HasValue)
                        entry.TextureView = descriptorEntry.TextureView.Value.Handle;

                    entries[i] = entry;
                }

                bindGroup.Entries = entries;
                bindGroup.EntryCount = (uint)descriptor.Entries.Length;
            }

            var handle = Api.DeviceCreateBindGroup(Handle, bindGroup);
            return new XBindGroup(handle);
        }
    }
    public XSwapChain CreateSwapChain(XSurface surface, XSwapChainDescriptor descriptor)
    {
        unsafe
        {
            var handle = Api.DeviceCreateSwapChain(Handle, surface.Handle, new SwapChainDescriptor
            {
                Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label),
                Format = descriptor.Format,
                Height = (uint)descriptor.Height,
                Width = (uint)descriptor.Width,
                Usage = descriptor.Usage,
                PresentMode = descriptor.PresentMode,
            });

            return new XSwapChain(handle);
        }
    }
    public XCommandEncoder CreateCommandEncoder(string? name = null)
    {
        unsafe
        {
            var handle = Api.DeviceCreateCommandEncoder(Handle, new CommandEncoderDescriptor
            {
                Label = (byte*)SilkMarshal.StringToPtr(name)
            });
            return new XCommandEncoder(handle);
        }
    }
    public XBuffer CreateBuffer(XBufferDescriptor descriptor)
    {
        unsafe
        {
            var buffer = new BufferDescriptor
            {
                MappedAtCreation = descriptor.MappedAtCreation,
                Size = (ulong)descriptor.Size,
                Usage = descriptor.Usage
            };

            if (!string.IsNullOrEmpty(descriptor.Label))
                buffer.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            var handle = Api.DeviceCreateBuffer(Handle, buffer);
            return new XBuffer(handle);
        }
    }
    public XTexture CreateTexture(XTextureDescriptor descriptor)
    {
        unsafe
        {
            var texture = new TextureDescriptor
            {
                Dimension = descriptor.Dimension,
                Format = descriptor.Format,
                MipLevelCount = (uint)descriptor.MipLevelCount,
                SampleCount = (uint)descriptor.SampleCount,
                Size = new Extent3D
                {
                    DepthOrArrayLayers = (uint)descriptor.Size.DepthOrArrayLayers,
                    Height = (uint)descriptor.Size.Height,
                    Width = (uint)descriptor.Size.Width,
                },
                Usage = descriptor.Usage
            };

            if (!string.IsNullOrEmpty(descriptor.Label))
                texture.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            if (descriptor.ViewFormats.Length != 0)
            {
                var viewFormats = stackalloc TextureFormat[descriptor.ViewFormats.Length];
                for (int i = 0; i < descriptor.ViewFormats.Length; i++)
                {
                    viewFormats[i] = descriptor.ViewFormats[i];
                }

                texture.ViewFormats = viewFormats;
                texture.ViewFormatCount = (uint)descriptor.ViewFormats.Length;
            }

            var handle = Api.DeviceCreateTexture(Handle, texture);
            return new XTexture(handle);
        }
    }
    public XSampler CreateSampler(XSamplerDescriptor descriptor)
    {
        unsafe
        {
            var sampler = new SamplerDescriptor
            {
                AddressModeU = descriptor.AddressModeU,
                AddressModeV = descriptor.AddressModeV,
                AddressModeW = descriptor.AddressModeW,
                Compare = descriptor.Compare,
                LodMaxClamp = descriptor.LodMaxClamp,
                LodMinClamp = descriptor.LodMinClamp,
                MagFilter = descriptor.MagFilter,
                MaxAnisotropy = (ushort)descriptor.MaxAnisotropy,
                MinFilter = descriptor.MinFilter,
                MipmapFilter = descriptor.MipmapFilter,
            };

            if (!string.IsNullOrEmpty(descriptor.Label))
                sampler.Label = (byte*)SilkMarshal.StringToPtr(descriptor.Label);

            var handle = Api.DeviceCreateSampler(Handle, sampler);
            return new XSampler(handle);
        }
    }
}
