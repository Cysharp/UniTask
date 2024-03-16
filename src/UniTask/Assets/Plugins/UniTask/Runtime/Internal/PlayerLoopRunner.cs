
namespace Cysharp.Threading.Tasks.Internal
{
    internal sealed class PlayerLoopRunner : ContinuationRunner
    {
        readonly PlayerLoopTiming timing;


        public PlayerLoopRunner(PlayerLoopTiming timing) : base()
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
                case PlayerLoopTiming.Initialization:
                    Initialization();
                    break;
                case PlayerLoopTiming.LastInitialization:
                    LastInitialization();
                    break;
                case PlayerLoopTiming.EarlyUpdate:
                    EarlyUpdate();
                    break;
                case PlayerLoopTiming.LastEarlyUpdate:
                    LastEarlyUpdate();
                    break;
                case PlayerLoopTiming.FixedUpdate:
                    FixedUpdate();
                    break;
                case PlayerLoopTiming.LastFixedUpdate:
                    LastFixedUpdate();
                    break;
                case PlayerLoopTiming.PreUpdate:
                    PreUpdate();
                    break;
                case PlayerLoopTiming.LastPreUpdate:
                    LastPreUpdate();
                    break;
                case PlayerLoopTiming.Update:
                    Update();
                    break;
                case PlayerLoopTiming.LastUpdate:
                    LastUpdate();
                    break;
                case PlayerLoopTiming.PreLateUpdate:
                    PreLateUpdate();
                    break;
                case PlayerLoopTiming.LastPreLateUpdate:
                    LastPreLateUpdate();
                    break;
                case PlayerLoopTiming.PostLateUpdate:
                    PostLateUpdate();
                    break;
                case PlayerLoopTiming.LastPostLateUpdate:
                    LastPostLateUpdate();
                    break;
#if UNITY_2020_2_OR_NEWER
                case PlayerLoopTiming.TimeUpdate:
                    TimeUpdate();
                    break;
                case PlayerLoopTiming.LastTimeUpdate:
                    LastTimeUpdate();
                    break;
#endif
                default:
                    break;
            }
#else
            RunCore();
#endif
        }

        void Initialization() => RunCore();
        void LastInitialization() => RunCore();
        void EarlyUpdate() => RunCore();
        void LastEarlyUpdate() => RunCore();
        void FixedUpdate() => RunCore();
        void LastFixedUpdate() => RunCore();
        void PreUpdate() => RunCore();
        void LastPreUpdate() => RunCore();
        void Update() => RunCore();
        void LastUpdate() => RunCore();
        void PreLateUpdate() => RunCore();
        void LastPreLateUpdate() => RunCore();
        void PostLateUpdate() => RunCore();
        void LastPostLateUpdate() => RunCore();
#if UNITY_2020_2_OR_NEWER
        void TimeUpdate() => RunCore();
        void LastTimeUpdate() => RunCore();
#endif
    }
}

