
namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class EngineCallbackRunner : ContinuationRunner
    {
        readonly EngineCallbackTiming timing;


        public EngineCallbackRunner(EngineCallbackTiming timing) : base()
        {
            this.timing = timing;
        }

        // delegate entrypoint.
        public void Run()
        {
            // for debugging, create named stacktrace.
#if DEBUG
            switch (timing)
            {
                case EngineCallbackTiming.OnBeforeRender:
                    OnBeforeRender();
                    break;
                case EngineCallbackTiming.WillRenderCanvases:
                    WillRenderCanvases();
                    break;
#if UNITY_2020_3_OR_NEWER
                case EngineCallbackTiming.PreWillRenderCanvases:
                    PreWillRenderCanvases();
                    break;
#endif

                default:
                    break;
            }
#else
            RunCore();
#endif
        }

        void OnBeforeRender() => RunCore();
        void WillRenderCanvases() => RunCore();
#if UNITY_2020_3_OR_NEWER
        void PreWillRenderCanvases() => RunCore();
#endif
    }
}
