//using Cysharp.Threading.Tasks.Internal;
//using System;
//using System.Collections.Concurrent;
//using System.Runtime.CompilerServices;
//using System.Threading;

//namespace Cysharp.Threading.Tasks
//{
//    public partial struct UniTask
//    {
//        public static UniTask Delay()
//        {
//            return default;
//        }

//        sealed class DelayPromise : IUniTaskSource
//        {
//            public void GetResult(short token)
//            {
//                throw new NotImplementedException();
//            }

//            public UniTaskStatus GetStatus(short token)
//            {
//                throw new NotImplementedException();
//            }

//            public void OnCompleted(Action<object> continuation, object state, short token)
//            {
//                throw new NotImplementedException();
//            }

//            public UniTaskStatus UnsafeGetStatus()
//            {
//                throw new NotImplementedException();
//            }
//        }
//    }
//}