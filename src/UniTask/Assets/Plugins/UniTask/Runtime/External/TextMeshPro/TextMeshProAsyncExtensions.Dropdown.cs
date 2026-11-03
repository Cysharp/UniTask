#if UNITASK_TEXTMESHPRO_SUPPORT

using System;
using System.Threading;
using TMPro;

namespace Cysharp.Threading.Tasks
{
    public static partial class TextMeshProAsyncExtensions
    {
        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this TMP_Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), false);
        }

        public static IAsyncValueChangedEventHandler<int> GetAsyncValueChangedEventHandler(this TMP_Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, false);
        }

        public static UniTask<int> OnValueChangedAsync(this TMP_Dropdown dropdown)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy(), true).OnInvokeAsync();
        }

        public static UniTask<int> OnValueChangedAsync(this TMP_Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new AsyncUnityEventHandler<int>(dropdown.onValueChanged, cancellationToken, true).OnInvokeAsync();
        }

        public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this TMP_Dropdown dropdown)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, dropdown.GetCancellationTokenOnDestroy());
        }

        public static IUniTaskAsyncEnumerable<int> OnValueChangedAsAsyncEnumerable(this TMP_Dropdown dropdown, CancellationToken cancellationToken)
        {
            return new UnityEventHandlerAsyncEnumerable<int>(dropdown.onValueChanged, cancellationToken);
        }
    }
}

#endif