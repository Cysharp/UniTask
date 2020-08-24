#if UNITASK_TEXTMESHPRO_SUPPORT

using System;
using System.Threading;
using TMPro;
using UnityEngine.Events;

namespace Cysharp.Threading.Tasks
{
    public static partial class TextMeshProAsyncExtensions
    {
        // <string> -> Text
        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TMP_Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo(this IUniTaskAsyncEnumerable<string> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore(IUniTaskAsyncEnumerable<string> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError)
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

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TMP_Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        public static void BindTo<T>(this IUniTaskAsyncEnumerable<T> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError = true)
        {
            BindToCore(source, text, cancellationToken, rebindOnError).Forget();
        }

        public static void BindTo<T>(this AsyncReactiveProperty<T> source, TMP_Text text, bool rebindOnError = true)
        {
            BindToCore(source, text, text.GetCancellationTokenOnDestroy(), rebindOnError).Forget();
        }

        static async UniTaskVoid BindToCore<T>(IUniTaskAsyncEnumerable<T> source, TMP_Text text, CancellationToken cancellationToken, bool rebindOnError)
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
    }
}

#endif