using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ExceptionExamples : MonoBehaviour
{
    
    private void Start()
    {

        //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        //ThrowFromAsyncVoid();
        //_ = ThrowFromTask();
        //_ = ThrowFromUniTask();
        
        //ThrowFromNonAsync();
    }

    public static async Task Test()
    {
        var webRequest = UnityWebRequest.Get("http");
        var request = webRequest.SendWebRequest();
        await request;
    }
}