using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Internal
{
    public enum PlayerLoopSystemEnum : int 
    {
#if UNITY_2020_2_OR_NEWER
        TimeUpdate,
#endif  
        Initialization,
        EarlyUpdate,
        FixedUpdate,
        PreUpdate,
        Update,
        PreLateUpdate,
        PostLateUpdate,
    }
}
