using Arch.Core;
using Enx.Engine.Arch;
using Enx.Engine.Arch.Groups;
using Enx.Engine.WebGPU.Components;
using Enx.WebGPU;
using Silk.NET.WebGPU;

namespace Enx.Engine.WebGPU.Systems;

public static partial class WebGPURenderSystem
{
    [UpdateInGroup<PreRenderSystemGroup>(OrderFirst = true)]
    public partial class PreRenderSystem(World world) : SystemBase(world)
    {
        public override void Update()
        {
            GetSwapChainViewQuery(World);
            CreateCommandEncoderQuery(World);
            BeginMainRenderPassQuery(World);
        }

        [Query]
        public void GetSwapChainView(in Entity entity, ref SwapChainData swapChain)
        {
            for (var attempt = 0; attempt < 2; attempt++)
            {
                swapChain.SwapChainView = swapChain.SwapChain.GetCurrentTextureView();

                if (attempt == 0 && swapChain.SwapChainView.IsEmpty)
                {
                    if (!swapChain.SwapChain.IsEmpty)
                        swapChain.SwapChain.Dispose();

                    var device = World.QueryUnique<XDevice>();
                    var surface = World.Get<SurfaceData>(entity);
                    swapChain.SwapChain = device.CreateSwapChain(surface.Surface, swapChain.Descriptor);

                    continue;
                }

                break;
            }

            if (swapChain.SwapChainView.IsEmpty)
                World.Add(entity, new SkipFrame());
            else if (World.Has<SkipFrame>(entity))
                World.Remove<SkipFrame>(entity);
        }

        [Query]
        [None(typeof(SkipFrame)), All(typeof(SwapChainData))]
        public void CreateCommandEncoder(in Entity entity)
        {
            var device = World.QueryUnique<XDevice>();
            World.Add(entity, device.CreateCommandEncoder());
        }

        [Query]
        [None(typeof(SkipFrame))]
        public void BeginMainRenderPass(in Entity entity, in XCommandEncoder encoder, in SwapChainData swapChain)
        {
            var renderPass = encoder.BeginRenderPass(new XRenderPassDescriptor
            {
                ColorAttachements =
                [
                    new XRenderPassColorAttachment
                    {
                        View = swapChain.SwapChainView,
                        LoadOp = LoadOp.Clear,
                        ClearValue = new Color(0, 1, 0, 1),
                        StoreOp = StoreOp.Store
                    }
                ]
            });

            World.Add(entity, renderPass);
        }
    }

    [UpdateInGroup<LateRenderSystemGroup>(OrderLast = true)]
    public partial class LateRenderSystem(World world) : SystemBase(world)
    {
        public override void Update()
        {
            FinishMainRenderPassQuery(World);
            FinishComamndEncoderQuery(World);
            SubmitToQueue();
            PresentSwapChainQuery(World);
        }

        [Query]
        [None(typeof(SkipFrame))]
        public void FinishMainRenderPass(in Entity entity, in XRenderPassEncoder renderPass)
        {
            renderPass.Dispose();
            World.Remove<XRenderPassEncoder>(entity);
        }

        [Query]
        [None(typeof(SkipFrame))]
        public void FinishComamndEncoder(in Entity entity, in XCommandEncoder commandEncoder)
        {
            var buffer = commandEncoder.Finish();
            commandEncoder.Dispose();
            World.Remove<XCommandEncoder>(entity);
            World.Add(entity, buffer);
        }

        private readonly QueryDescription _submitToQueueDescription = new QueryDescription()
            .WithAll<XCommandBuffer>()
            .WithNone<SkipFrame>();

        public void SubmitToQueue()
        {
            var queue = World.QueryUnique<XQueue>();
            var query = World.Query(_submitToQueueDescription);

            var buffers = new List<XCommandBuffer>();

            foreach (var buffer in query.GetComponentsIterator<XCommandBuffer>())
                buffers.Add(buffer.t0);

            World.Remove<XCommandBuffer>(_submitToQueueDescription);

            queue.Submit(buffers.ToArray());
        }

        [Query]
        public static void PresentSwapChain(ref SwapChainData swapChain)
        {
            if (!swapChain.SwapChainView.IsEmpty)
            {
                swapChain.SwapChain.Present();
                swapChain.SwapChainView.Dispose();
                swapChain.SwapChainView = default;
            }
        }
    }
}
