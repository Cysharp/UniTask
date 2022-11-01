using System;
using System.Threading;
using UnityEngine;
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT
using UnityEngine.UI;
#endif

namespace Cysharp.Threading.Tasks
{
    public static class UnityBindingExtensions
    {
#if !UNITY_2019_1_OR_NEWER || UNITASK_UGUI_SUPPORT
        // <string> -> Text

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, UnityEngine.UI.Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, UnityEngine.UI.Text text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, UnityEngine.UI.Text text, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    text.text = e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // <T> -> Text

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, UnityEngine.UI.Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, UnityEngine.UI.Text text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError).Forget();
        }

        public static void BindTo<T>(this AsyncReactiveProperty<T> source, UnityEngine.UI.Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, UnityEngine.UI.Text text, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    text.text = e.Current.ToString();
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        // <bool> -> Selectable

        public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, bool rebindOnError = true)
        {
            BindToCore(source, selectable, selectable.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, selectable, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<bool> source, Selectable selectable, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;


                    selectable.interactable = e.Current;
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
#endif

        // <T> -> Action

        public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject monoBehaviour, Action<TObject, TSource> bindAction, bool rebindOnError = true)
            where TObject : MonoBehaviour
        {
            BindToCore(source, monoBehaviour, bindAction, monoBehaviour.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo<TSource, TObject>(this IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, bindTarget, bindAction, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<TSource, TObject>(IUniTaskAsyncEnumerable<TSource> source, TObject bindTarget, Action<TObject, TSource> bindAction, CancellationToken cancellationToken, bool rebindOnError)
        {
            var repeat = false;
            BIND_AGAIN:
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (true)
                {
                    bool moveNext;
                    try
                    {
                        moveNext = await e.MoveNextAsync();
                        repeat = false;
                    }
                    catch (Exception ex)
                    {
                        if (ex is OperationCanceledException) return;

                        if (rebindOnError && !repeat)
                        {
                            repeat = true;
                            goto BIND_AGAIN;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (!moveNext) return;

                    bindAction(bindTarget, e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }
}
