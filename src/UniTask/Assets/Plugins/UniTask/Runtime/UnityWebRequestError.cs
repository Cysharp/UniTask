#if ENABLE_UNITYWEBREQUEST

using System;
using UnityEngine.Networking;

namespace Cysharp.Threading.Tasks
{
    public class UnityWebRequestException : Exception
    {
        public UnityWebRequest UnityWebRequest { get; }
        public bool IsNetworkError { get; }
        public bool IsHttpError { get; }

        public UnityWebRequestException(UnityWebRequest unityWebRequest)
            : base(unityWebRequest.error + Environment.NewLine + unityWebRequest.downloadHandler.text)
        {
            this.UnityWebRequest = unityWebRequest;
            this.IsNetworkError = unityWebRequest.isNetworkError;
            this.IsHttpError = unityWebRequest.isHttpError;
        }
    }
}

#endif