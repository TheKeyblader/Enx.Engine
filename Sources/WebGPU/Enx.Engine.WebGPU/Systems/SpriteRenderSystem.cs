using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.Asset;
using Enx.Engine.Components;
using Enx.Engine.Graphics.Assets;
using Enx.Engine.Graphics.Components;
using Enx.Engine.WebGPU.Components;
using Enx.Engine.WebGPU.Helpers;
using Enx.WebGPU;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using System.Buffers;

namespace Enx.Engine.WebGPU.Systems;

[UpdateInGroup<RenderSystemGroup>]
public partial class SpriteRenderSystem(World world, IAssetManager assetManager) : SystemBase(world)
{
    private const int MaxBatchSize = 2048;
    private const int MinBatchSize = 128;

    private readonly IAssetManager _assetManager = assetManager;

    private XDevice Device;
    private XBuffer IndexBuffer;
    private XRenderPipeline RenderPipeline;

    //Textures
    private XBindGroupLayout TextureLayout;
    private XSampler DefaultSampler;

    //Instances
    private readonly List<SpriteInstance> Instances = new(MinBatchSize);
    private readonly List<XBuffer> Buffers = new(1);
    private readonly Dictionary<HandleId, Asset.Handle<GpuImage>> _textureMappings = new();
    private readonly Dictionary<HandleId, XBindGroup> _bindMappings = new();
    private int spriteRendered;
    private int UsedBuffers;

    public override void Update()
    {
        if (IndexBuffer.IsEmpty)
        {
            Device = World.QueryUnique<XDevice>();
            CreateIndexBuffer();
            CreateTextureLayout();
            CreateRenderPipeline();
        }

        Instances.Clear();
        UsedBuffers = 0;
        spriteRendered = 0;
        ExtractSpritesQuery(World);
        RenderSprite();
        TrimBuffers();
    }

    public void CreateIndexBuffer()
    {
        IndexBuffer = Device.CreateBuffer(new XBufferDescriptor
        {
            Label = "Sprite Index Buffer",
            Usage = BufferUsage.Index,
            Size = sizeof(ushort) * 6,
            MappedAtCreation = true,
        });

        var indexes = IndexBuffer.GetMappedRange<ushort>(0, 6);
        indexes[0] = 2;
        indexes[1] = 0;
        indexes[2] = 1;
        indexes[3] = 1;
        indexes[4] = 3;
        indexes[5] = 2;

        IndexBuffer.Unmap();
    }
    public void CreateTextureLayout()
    {
        TextureLayout = Device.CreateBindGroupLayout(new XBindGroupLayoutDescriptor
        {
            Label = "Sprite bind layout",
            Entries =
            [
                new XBindGroupLayoutEntry
                {
                    Binding = 0,
                    Visibility = ShaderStage.Fragment,
                    Texture = new XTextureBindingLayout
                    {
                        Multisampled = false,
                        SampleType = TextureSampleType.Float,
                        ViewDimension = TextureViewDimension.Dimension2D
                    }
                },
                new XBindGroupLayoutEntry
                {
                    Binding = 1,
                    Visibility = ShaderStage.Fragment,
                    Sampler = SamplerBindingType.Filtering
                }
            ]
        });
        DefaultSampler = Device.CreateSampler(XSamplerDescriptor.Linear);
    }
    public void CreateRenderPipeline()
    {
        var cameraLayout = World.QueryUnique<CameraLayout>();
        var preferredFormat = World.QueryUnique<PreferredFormat>();

        XShaderModule shader;
        {
            var assembly = typeof(SpriteRenderSystem).Assembly;
            using var stream = assembly.GetManifestResourceStream("Enx.Engine.WebGPU.Shaders.sprite.wgsl")!;
            using var reader = new StreamReader(stream);
            shader = Device.CreateWGSLShader(new XShaderModuleWGSLDescriptor
            {
                Code = reader.ReadToEnd(),
                Descriptor = new XShaderModuleDescriptor
                {
                    Label = "Sprite Shader"
                }
            });
        }

        var layout = Device.CreatePipelineLayout(new XPipelineLayoutDescriptor
        {
            Label = "Sprite Pipeline Layout",
            BindGroupLayouts = [cameraLayout.Value, TextureLayout]
        });
        RenderPipeline = Device.CreateRenderPipeline(new XRenderPipelineDescriptor
        {
            Label = "Sprite RenderPipeline",
            Layout = layout,
            Vertex = new XVertexState
            {
                Module = shader,
                EntryPoint = "vs_main",
                Buffers =
                [
                    new XVertexBufferLayout
                    {
                        ArrayStride = UnsafeHelpers.BufferAlign<SpriteVertex>(),
                        StepMode = VertexStepMode.Instance,
                        Attributes =
                        [
                            new XVertexAttribute
                            {
                                ShaderLocation = 0,
                                Format = VertexFormat.Float32x4,
                                Offset = 0
                            },
                            new XVertexAttribute
                            {
                                ShaderLocation = 1,
                                Format = VertexFormat.Float32x4,
                                Offset = UnsafeHelpers.BufferAlign<Vector4D<float>>(),
                            },
                            new XVertexAttribute
                            {
                                ShaderLocation = 2,
                                Format = VertexFormat.Float32x4,
                                Offset = UnsafeHelpers.BufferAlign<Vector4D<float>>() * 2,
                            },
                            new XVertexAttribute
                            {
                                ShaderLocation = 3,
                                Format = VertexFormat.Float32x4,
                                Offset = UnsafeHelpers.BufferAlign<Vector4D<float>>() * 3,
                            },
                            new XVertexAttribute
                            {
                                ShaderLocation = 4,
                                Format = VertexFormat.Float32x4,
                                Offset = UnsafeHelpers.BufferAlign<Vector4D<float>>() * 4,
                            },
                            new XVertexAttribute
                            {
                                ShaderLocation = 5,
                                Format = VertexFormat.Float32x4,
                                Offset = UnsafeHelpers.BufferAlign<Vector4D<float>>() * 5,
                            },
                        ]
                    }
                ]
            },
            Fragment = new XFragmentState
            {
                Module = shader,
                EntryPoint = "fs_main",
                Targets =
                [
                    new XColorTargetState
                    {
                        Blend = XBlendState.AlphaBlending,
                        Format = preferredFormat.Value,
                        WriteMask = ColorWriteMask.All
                    }
                ]
            },
            Primitive = new XPrimitiveState
            {
                CullMode = CullMode.None,
                FrontFace = FrontFace.Ccw,
                StripIndexFormat = IndexFormat.Undefined,
                Topology = PrimitiveTopology.TriangleList,
            },
            Multisample = new XMultisampleState
            {
                AlphaToCoverageEnabled = false,
                Count = 1,
                Mask = ~0u
            }
        });
    }

    [Query]
    public void ExtractSprites(in Sprite sprite, in Transform transform)
    {
        if (_assetManager.GetLoadState(sprite.ImageHandle) is not (LoadState.Loaded or LoadState.Unloaded))
            return;

        if (!_textureMappings.TryGetValue(sprite.ImageHandle, out var gpuImageId))
        {
            _textureMappings[sprite.ImageHandle] = _assetManager.Transform<Image, GpuImage>(sprite.ImageHandle, false);
            return;
        }

        if (_assetManager.GetLoadState(gpuImageId) is not LoadState.Loaded)
            return;

        var gpuImage = _assetManager.GetAsset<GpuImage>(gpuImageId);

        var imageSize = gpuImage.Size;
        var quadSize = gpuImage.Size.As<float>();
        Vector4D<float> uv_offset_scale;

        if (sprite.SourceRect.HasValue)
        {
            var rect = sprite.SourceRect.Value;
            uv_offset_scale = new Vector4D<float>
                (rect.Origin.X / imageSize.X,
                rect.Max.Y / imageSize.Y,
                rect.Size.X / imageSize.X,
                -rect.Size.Y / imageSize.Y);
            quadSize = rect.Size;
        }
        else
        {
            uv_offset_scale = new(0, 1, 1, 1);
        }

        if (sprite.FlipX)
        {
            uv_offset_scale.X += uv_offset_scale.Z;
            uv_offset_scale.Z *= -1;
        }
        if (sprite.FlipY)
        {
            uv_offset_scale.Y += uv_offset_scale.Z;
            uv_offset_scale.W *= -1;
        }

        if (sprite.CustomSize.HasValue)
            quadSize = sprite.CustomSize.Value;

        var matrix = transform.Value;
        var order = 0f;
        if (Matrix4X4.Decompose(matrix, out var scale, out var rotation, out var translation))
        {
            matrix = Matrix4X4<float>.Identity;
            matrix *= Matrix4X4.CreateScale(scale * new Vector3D<float>(quadSize, 1));
            matrix *= Matrix4X4.CreateFromQuaternion(rotation * Quaternion<float>.Identity);
            matrix *= Matrix4X4.CreateTranslation(translation + new Vector3D<float>(quadSize * (-sprite.Origin - new Vector2D<float>(0.5f)), 0));
            order = translation.Z;
        }

        Instances.Add(new SpriteInstance
        {
            Transform = matrix,
            TexCoordOffsetScale = uv_offset_scale,
            TextureId = sprite.ImageHandle,
            Color = sprite.Color.ToLinearVector(),
            Order = order
        });
    }

    private QueryDescription RenderPassQuery = new QueryDescription()
        .WithAll<XRenderPassEncoder>();

    private QueryDescription CameraQuery = new QueryDescription()
        .WithAll<GpuCamera>();

    public void RenderSprite()
    {
        if (Instances.Count == 0) return;

        Instances.Sort();

        foreach (var _renderPass in World.Query(RenderPassQuery).GetComponentsIterator<XRenderPassEncoder>())
        {
            var renderPass = _renderPass.t0;
            renderPass.SetPipeline(RenderPipeline);
            renderPass.SetIndexBuffer(IndexBuffer, IndexFormat.Uint16);

            foreach (var _camera in World.Query(CameraQuery).GetComponentsIterator<GpuCamera>())
            {
                var camera = _camera.t0;

                renderPass.SetBindGroup(0, camera.BindGroup);

                HandleId batchTexture = default;
                int batchStart = 0;

                for (var pos = 0; pos < Instances.Count; pos++)
                {
                    var texture = Instances[pos].TextureId;

                    if (texture != batchTexture)
                    {
                        if (pos > batchStart)
                        {
                            RenderBatch(renderPass, batchTexture, batchStart, pos - batchStart);
                        }

                        batchTexture = texture;
                        batchStart = pos;
                    }
                }

                RenderBatch(renderPass, batchTexture, batchStart, Instances.Count - batchStart);
            }
        }
    }

    public void RenderBatch(XRenderPassEncoder renderPass, HandleId textureId, int offset, int length)
    {
        var gpuImageId = _textureMappings[textureId];
        var gpuImage = _assetManager.GetAsset<GpuImage>(gpuImageId);
        if (!_bindMappings.TryGetValue(gpuImageId, out var bindGroup))
        {
            _bindMappings[gpuImageId] = bindGroup = Device.CreateBindGroup(new XBindGroupDescriptor
            {
                Layout = TextureLayout,
                Entries =
                [
                    new XBindGroupEntry
                    {
                        Binding = 0,
                        TextureView = gpuImage.TextureView,
                    },
                    new XBindGroupEntry
                    {
                        Binding = 1,
                        Sampler = DefaultSampler
                    }
                ]
            });
        }

        renderPass.SetBindGroup(1, bindGroup);

        while (length > 0)
        {
            var batchSize = length;
            int remainingSpace = MaxBatchSize - spriteRendered;

            if (batchSize > remainingSpace)
            {
                if (remainingSpace < MinBatchSize)
                {
                    spriteRendered = 0;
                    batchSize = Math.Min(length, MaxBatchSize);
                }
                else
                {
                    batchSize = remainingSpace;
                }
            }

            if (spriteRendered == 0)
            {
                if (Buffers.Count < UsedBuffers + 1)
                {
                    Buffers.Add(Device.CreateBuffer(new XBufferDescriptor
                    {
                        Label = $"Sprite Buffer Batch #{UsedBuffers}",
                        Size = UnsafeHelpers.BufferAlign<SpriteVertex>() * MaxBatchSize,
                        Usage = BufferUsage.Vertex | BufferUsage.CopyDst
                    }));
                }
                UsedBuffers++;
            }

            var buffer = Buffers[UsedBuffers - 1];
            using var owner = MemoryPool<SpriteVertex>.Shared.Rent(batchSize);
            for (var i = 0; i < batchSize; i++)
            {
                var sprite = Instances[offset + i];
                owner.Memory.Span[spriteRendered + i] = new SpriteVertex
                {
                    Transform = sprite.Transform,
                    Color = sprite.Color,
                    TexCoordOffsetScale = sprite.TexCoordOffsetScale,
                };
            }
            var size = UnsafeHelpers.BufferAlign<SpriteVertex>();
            Device.Queue.WriteBuffer(buffer, spriteRendered, owner.Memory.Span[..batchSize]);
            renderPass.SetVertexBuffer(0, buffer, size * spriteRendered, size * batchSize);
            renderPass.DrawIndexed(6, batchSize, 0, 0, spriteRendered);

            spriteRendered += batchSize;
            offset += batchSize;
            length -= batchSize;
        }
    }

    public void TrimBuffers()
    {
        for (int i = UsedBuffers; i < Buffers.Count; i++)
        {
            var buffer = Buffers[^1];
            Buffers.Remove(buffer);
            buffer.Dispose();
        }
    }
}


struct SpriteInstance : IComparable<SpriteInstance>
{
    public HandleId TextureId;
    public Matrix4X4<float> Transform;
    public Vector4D<float> Color;
    public Vector4D<float> TexCoordOffsetScale;
    public float Order;

    public readonly int CompareTo(SpriteInstance other)
        => Order.CompareTo(other.Order);
}

struct SpriteVertex
{
    public Matrix4X4<float> Transform;
    public Vector4D<float> Color;
    public Vector4D<float> TexCoordOffsetScale;
}

