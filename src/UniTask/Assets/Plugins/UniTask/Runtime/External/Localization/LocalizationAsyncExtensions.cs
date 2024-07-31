#if UNITASK_LOCALIZATION_SUPPORT

using Cysharp.Threading.Tasks.Linq;
using UnityEngine.Localization;

namespace Cysharp.Threading.Tasks
{
    public static partial class LocalizationAsyncExtensions
    {
        public static IUniTaskAsyncEnumerable<string> OnValueChangedAsAsyncEnumerable(
            this LocalizedString localizedString)
        {
            return UniTaskAsyncEnumerable.Create<string>(async (writer, cancellationToken) =>
            {
                async void Handler(string newValue)
                {
                    await writer.YieldAsync(newValue);
                }

                localizedString.StringChanged += Handler;
                cancellationToken.Register(() => localizedString.StringChanged -= Handler);

                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Yield();
                }
            });
        }
    }
}

#endif