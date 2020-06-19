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
        public string Error { get; }
        public string Text { get; }

        string msg;

        public UnityWebRequestException(UnityWebRequest unityWebRequest)
        {
            this.UnityWebRequest = unityWebRequest;
            this.IsNetworkError = unityWebRequest.isNetworkError;
            this.IsHttpError = unityWebRequest.isHttpError;
            this.Error = unityWebRequest.error;
            this.Text = unityWebRequest.downloadHandler.text;
        }

        public override string Message
        {
            get
            {
                if (msg == null)
                {
                    msg = Error + Environment.NewLine + Text;
                }
                return msg;
            }
        }
    }
}

#endif