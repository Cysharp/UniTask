using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExceptionExamples : MonoBehaviour
{
    public Button ButtonTest;

    void Start()
    {
        ButtonTest.OnClickAsAsyncEnumerable()
            .Subscribe(async _ =>
            {
                try
                {
                    await new Foo().MethodFooAsync();
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                }
            }, this.GetCancellationTokenOnDestroy());
    }
}

class Foo
{
    public async UniTask MethodFooAsync()
    {
        await MethodBarAsync();
    }

    private async UniTask MethodBarAsync()
    {
        Throw();
    }

    private void Throw()
    {
        throw new Exception();
    }
}