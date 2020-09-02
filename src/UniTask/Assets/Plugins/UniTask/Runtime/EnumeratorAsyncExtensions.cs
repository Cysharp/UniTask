#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
    public static class EnumeratorAsyncExtensions
    {
        public static UniTask.Awaiter GetAwaiter<T>(this T enumerator)
            where T : IEnumerator
        {
            var e = (IEnumerator)enumerator;
            Error.ThrowArgumentNullException(e, nameof(enumerator));
            return StartCoroutineAsUniTask(enumerator).GetAwaiter();
        }

        public static UniTask WithCancellation(this IEnumerator enumerator, CancellationToken cancellationToken)
        {
            Error.ThrowArgumentNullException(enumerator, nameof(enumerator));
            return StartCoroutineAsUniTask(enumerator, cancellationToken: cancellationToken);
        }

        public static UniTask ToUniTask(this IEnumerator enumerator, PlayerLoopTiming? timing = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Error.ThrowArgumentNullException(enumerator, nameof(enumerator));
            return StartCoroutineAsUniTask(enumerator, timing, cancellationToken);
        }

        static async UniTask StartCoroutineAsUniTask(IEnumerator enumerator, PlayerLoopTiming? timing = default, CancellationToken cancellationToken = default)
        {
            Exception exception = null;
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                while (enumerator.MoveNext())
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var current = enumerator.Current;
                    switch (current)
                    {
                        case null:
                        default:
                            {
                                await UniTask.Yield(PlayerLoopTiming.LastUpdate);
                                break;
                            }
                        case WaitForFixedUpdate waitForFixedUpdate:
                            {
                                await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
                                break;
                            }
                        case WaitForEndOfFrame waitForEndOfFrame:
                            {
                                await UniTask.WaitForEndOfFrame();
                                break;
                            }
                        case WaitForSeconds waitForSeconds:
                            {
                                var second = (float)waitForSeconds_Seconds.GetValue(waitForSeconds);
                                var elapsed = 0.0f;
                                do
                                {
                                    await UniTask.Yield(PlayerLoopTiming.LastUpdate);
                                    cancellationToken.ThrowIfCancellationRequested();
                                    elapsed += Time.deltaTime;
                                } while (elapsed < second);
                                break;
                            }
                        case CustomYieldInstruction cyi: // Include WWW, WaitForSecondsRealtime
                            {
                                while (cyi.keepWaiting)
                                {
                                    await UniTask.Yield(PlayerLoopTiming.LastUpdate);
                                    cancellationToken.ThrowIfCancellationRequested();
                                }
                                break;
                            }
                        case IEnumerator innerEnumerator:
                            {
                                await StartCoroutineAsUniTask(innerEnumerator, null, cancellationToken);
                                break;
                            }

                        case AsyncOperation ao:
                            {
                                await ao;
                                break;
                            }

                        case YieldInstruction yieldInstruction:
                            {
                                bool isKnownIssue;
                                switch (yieldInstruction)
                                {
                                    case Coroutine coroutine:
                                        {
                                            isKnownIssue = true;
                                            break;
                                        }
                                    default:
                                        {
                                            isKnownIssue = false;
                                            break;
                                        }
                                }
                                throw new NotSupportedException("Coroutine yields YieldInstruction of type \"" + yieldInstruction.GetType().FullName + (isKnownIssue ? "\". Which is not supported by UniTask." : "\". Which is unknown by UniTask."));
                            }
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch(Exception e)
            {
                exception = e;
                throw;
            }
            finally
            {
                if (timing is PlayerLoopTiming playerLoopTiming && PlayerLoopHelper.TryGetCurrentPlayerLoopTiming() != playerLoopTiming) // To avoid unintentional one frame of extra latency
                {
                    await UniTask.Yield(playerLoopTiming);

                }
                if(exception is null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

        }

        static readonly FieldInfo waitForSeconds_Seconds = typeof(WaitForSeconds).GetField("m_Seconds", BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic);
    }
}

