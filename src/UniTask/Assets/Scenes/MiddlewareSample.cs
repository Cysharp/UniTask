//using Cysharp.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//namespace Cysharp.Threading.Tasks.Sample
//{
//    //public class Sample2
//    //{
//    //    public Sample2()
//    //    {
//    //        // デコレーターの詰まったClientを生成（これは一度作ったらフィールドに保存可）
//    //        var client = new NetworkClient("http://localhost", TimeSpan.FromSeconds(10),
//    //            new QueueRequestDecorator(),
//    //            new LoggingDecorator(),
//    //            new AppendTokenDecorator(),
//    //            new SetupHeaderDecorator());


//    //        await client.PostAsync("/User/Register", new { Id = 100 });


//    //    }
//    //}


//    public class ReturnToTitleDecorator : IAsyncDecorator
//    {
//        public async UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//        {
//            try
//            {
//                return await next(context, cancellationToken);
//            }
//            catch (Exception ex)
//            {
//                if (ex is OperationCanceledException)
//                {
//                    // キャンセルはきっと想定されている処理なのでそのまんまスルー（呼び出し側でOperationCanceledExceptionとして飛んでいく)
//                    throw;
//                }

//                if (ex is UnityWebRequestException uwe)
//                {
//                    // ステータスコードを使って、タイトルに戻す例外です、とかリトライさせる例外です、とかハンドリングさせると便利
//                    // if (uwe.ResponseCode) { }...
//                }

//                // サーバー例外のMessageを直接出すなんて乱暴なことはデバッグ時だけですよ勿論。
//                var result = await MessageDialog.ShowAsync(ex.Message);

//                // OK か Cancelかで分岐するなら。今回はボタン一個、OKのみの想定なので無視
//                // if (result == DialogResult.Ok) { }...

//                // シーン呼び出しはawaitしないこと！awaitして正常終了しちゃうと、この通信の呼び出し元に処理が戻って続行してしまいます
//                // のでForget。
//                SceneManager.LoadSceneAsync("TitleScene").ToUniTask().Forget();


//                // そしてOperationCanceledExceptionを投げて、この通信の呼び出し元の処理はキャンセル扱いにして終了させる
//                throw new OperationCanceledException();
//            }
//        }
//    }

//    public enum DialogResult
//    {
//        Ok,
//        Cancel
//    }

//    public static class MessageDialog
//    {
//        public static async UniTask<DialogResult> ShowAsync(string message)
//        {
//            // (例えば)Prefabで作っておいたダイアログを生成する
//            var view = await Resources.LoadAsync("Prefabs/Dialog");

//            // Ok, Cancelボタンのどちらかが押されるのを待機
//            return await (view as GameObject).GetComponent<MessageDialogView>().ClickResult;
//        }
//    }

//    public class MessageDialogView : MonoBehaviour
//    {
//        [SerializeField] Button okButton = default;
//        [SerializeField] Button closeButton = default;

//        UniTaskCompletionSource<DialogResult> taskCompletion;

//        // これでどちらかが押されるまで無限に待つを表現
//        public UniTask<DialogResult> ClickResult => taskCompletion.Task;

//        private void Start()
//        {
//            taskCompletion = new UniTaskCompletionSource<DialogResult>();

//            okButton.onClick.AddListener(() =>
//            {
//                taskCompletion.TrySetResult(DialogResult.Ok);
//            });

//            closeButton.onClick.AddListener(() =>
//            {
//                taskCompletion.TrySetResult(DialogResult.Cancel);
//            });
//        }

//        // もしボタンが押されずに消滅した場合にネンノタメ。
//        private void OnDestroy()
//        {
//            taskCompletion.TrySetResult(DialogResult.Cancel);
//        }
//    }

//    public class MockDecorator : IAsyncDecorator
//    {
//        Dictionary<string, object> mock;

//        // Pathと型を1:1にして事前定義したオブジェクトを返す辞書を渡す
//        public MockDecorator(Dictionary<string, object> mock)
//        {
//            this.mock = mock;
//        }

//        public UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//        {
//            if (mock.TryGetValue(context.Path, out var value))
//            {
//                // 一致したものがあればそれを返す（実際の通信は行わない）
//                return new UniTask<ResponseContext>(new ResponseContext(value));
//            }
//            else
//            {
//                return next(context, cancellationToken);
//            }
//        }
//    }

//    //public class LoggingDecorator : IAsyncDecorator
//    //{
//    //    public async UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//    //    {
//    //        var sw = Stopwatch.StartNew();
//    //        try
//    //        {
//    //            UnityEngine.Debug.Log("Start Network Request:" + context.Path);

//    //            var response = await next(context, cancellationToken);

//    //            UnityEngine.Debug.Log($"Complete Network Request: {context.Path} , Elapsed: {sw.Elapsed}, Size: {response.GetRawData().Length}");

//    //            return response;
//    //        }
//    //        catch (Exception ex)
//    //        {
//    //            if (ex is OperationCanceledException)
//    //            {
//    //                UnityEngine.Debug.Log("Request Canceled:" + context.Path);
//    //            }
//    //            else if (ex is TimeoutException)
//    //            {
//    //                UnityEngine.Debug.Log("Request Timeout:" + context.Path);
//    //            }
//    //            else if (ex is UnityWebRequestException webex)
//    //            {
//    //                if (webex.IsHttpError)
//    //                {
//    //                    UnityEngine.Debug.Log($"Request HttpError: {context.Path} Code:{webex.ResponseCode} Message:{webex.Message}");
//    //                }
//    //                else if (webex.IsNetworkError)
//    //                {
//    //                    UnityEngine.Debug.Log($"Request NetworkError: {context.Path} Code:{webex.ResponseCode} Message:{webex.Message}");
//    //                }
//    //            }
//    //            throw;
//    //        }
//    //        finally
//    //        {
//    //            /* log other */
//    //        }
//    //    }
//    //}

//    public class SetupHeaderDecorator : IAsyncDecorator
//    {
//        public async UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//        {
//            context.RequestHeaders["x-app-timestamp"] = context.Timestamp.ToString();
//            context.RequestHeaders["x-user-id"] = "132141411"; // どこかから持ってくる
//            context.RequestHeaders["x-access-token"] = "fafafawfafewaea"; // どこかから持ってくる2

//            var respsonse = await next(context, cancellationToken);

//            var nextToken = respsonse.ResponseHeaders["token"];
//            // UserProfile.Token = nextToken; // どこかにセットするということにする

//            return respsonse;
//        }
//    }


//    public class AppendTokenDecorator : IAsyncDecorator
//    {
//        public async UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//        {
//            string token = "token"; // どっかから取ってくるということにする
//            RETRY:
//            try
//            {
//                context.RequestHeaders["x-access-token"] = token;
//                return await next(context, cancellationToken);
//            }
//            catch (UnityWebRequestException ex)
//            {
//                // 例えば700はTokenを再取得してください的な意味だったとする
//                if (ex.ResponseCode == 700)
//                {
//                    // 別口でTokenを取得します的な処理
//                    var newToken = await new NetworkClient(context.BasePath, context.Timeout).PostAsync<string>("/Auth/GetToken", "access_token", cancellationToken);
//                    context.Reset(this);
//                    goto RETRY;
//                }

//                goto RETRY;
//            }
//        }
//    }

//    public class QueueRequestDecorator : IAsyncDecorator
//    {
//        readonly Queue<(UniTaskCompletionSource<ResponseContext>, RequestContext, CancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>>)> q = new Queue<(UniTaskCompletionSource<ResponseContext>, RequestContext, CancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>>)>();
//        bool running;

//        public async UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next)
//        {
//            if (q.Count == 0)
//            {
//                return await next(context, cancellationToken);
//            }
//            else
//            {
//                var completionSource = new UniTaskCompletionSource<ResponseContext>();
//                q.Enqueue((completionSource, context, cancellationToken, next));
//                if (!running)
//                {
//                    Run().Forget();
//                }
//                return await completionSource.Task;
//            }
//        }

//        async UniTaskVoid Run()
//        {
//            running = true;
//            try
//            {
//                while (q.Count != 0)
//                {
//                    var (tcs, context, cancellationToken, next) = q.Dequeue();
//                    try
//                    {
//                        var response = await next(context, cancellationToken);
//                        tcs.TrySetResult(response);
//                    }
//                    catch (Exception ex)
//                    {
//                        tcs.TrySetException(ex);
//                    }
//                }
//            }
//            finally
//            {
//                running = false;
//            }
//        }
//    }


//    public class RequestContext
//    {
//        int decoratorIndex;
//        readonly IAsyncDecorator[] decorators;
//        Dictionary<string, string> headers;

//        public string BasePath { get; }
//        public string Path { get; }
//        public object Value { get; }
//        public TimeSpan Timeout { get; }
//        public DateTimeOffset Timestamp { get; private set; }

//        public IDictionary<string, string> RequestHeaders
//        {
//            get
//            {
//                if (headers == null)
//                {
//                    headers = new Dictionary<string, string>();
//                }
//                return headers;
//            }
//        }

//        public RequestContext(string basePath, string path, object value, TimeSpan timeout, IAsyncDecorator[] filters)
//        {
//            this.decoratorIndex = -1;
//            this.decorators = filters;
//            this.BasePath = basePath;
//            this.Path = path;
//            this.Value = value;
//            this.Timeout = timeout;
//            this.Timestamp = DateTimeOffset.UtcNow;
//        }

//        internal Dictionary<string, string> GetRawHeaders() => headers;
//        internal IAsyncDecorator GetNextDecorator() => decorators[++decoratorIndex];

//        public void Reset(IAsyncDecorator currentFilter)
//        {
//            decoratorIndex = Array.IndexOf(decorators, currentFilter);
//            if (headers != null)
//            {
//                headers.Clear();
//            }
//            Timestamp = DateTimeOffset.UtcNow;
//        }
//    }

//    public class ResponseContext
//    {
//        bool hasValue;
//        object value;
//        readonly byte[] bytes;

//        public long StatusCode { get; }
//        public Dictionary<string, string> ResponseHeaders { get; }

//        public ResponseContext(object value, Dictionary<string, string> header = null)
//        {
//            this.hasValue = true;
//            this.value = value;
//            this.StatusCode = 200;
//            this.ResponseHeaders = (header ?? new Dictionary<string, string>());
//        }

//        public ResponseContext(byte[] bytes, long statusCode, Dictionary<string, string> responseHeaders)
//        {
//            this.hasValue = false;
//            this.bytes = bytes;
//            this.StatusCode = statusCode;
//            this.ResponseHeaders = responseHeaders;
//        }

//        public byte[] GetRawData() => bytes;

//        public T GetResponseAs<T>()
//        {
//            if (hasValue)
//            {
//                return (T)value;
//            }

//            value = JsonUtility.FromJson<T>(Encoding.UTF8.GetString(bytes));
//            hasValue = true;
//            return (T)value;
//        }
//    }

//    public interface IAsyncDecorator
//    {
//        UniTask<ResponseContext> SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next);
//    }


//    public class NetworkClient : IAsyncDecorator
//    {
//        readonly Func<RequestContext, CancellationToken, UniTask<ResponseContext>> next;
//        readonly IAsyncDecorator[] decorators;
//        readonly TimeSpan timeout;
//        readonly IProgress<float> progress;
//        readonly string basePath;

//        public NetworkClient(string basePath, TimeSpan timeout, params IAsyncDecorator[] decorators)
//            : this(basePath, timeout, null, decorators)
//        {
//        }

//        public NetworkClient(string basePath, TimeSpan timeout, IProgress<float> progress, params IAsyncDecorator[] decorators)
//        {
//            this.next = InvokeRecursive; // setup delegate

//            this.basePath = basePath;
//            this.timeout = timeout;
//            this.progress = progress;
//            this.decorators = new IAsyncDecorator[decorators.Length + 1];
//            Array.Copy(decorators, this.decorators, decorators.Length);
//            this.decorators[this.decorators.Length - 1] = this;
//        }

//        public async UniTask<T> PostAsync<T>(string path, T value, CancellationToken cancellationToken = default)
//        {
//            var request = new RequestContext(basePath, path, value, timeout, decorators);
//            var response = await InvokeRecursive(request, cancellationToken);
//            return response.GetResponseAs<T>();
//        }


//        UniTask<ResponseContext> InvokeRecursive(RequestContext context, CancellationToken cancellationToken)
//        {
//            return context.GetNextDecorator().SendAsync(context, cancellationToken, next); // マジカル再帰処理
//        }

//        async UniTask<ResponseContext> IAsyncDecorator.SendAsync(RequestContext context, CancellationToken cancellationToken, Func<RequestContext, CancellationToken, UniTask<ResponseContext>> _)
//        {
//            // Postしか興味ないからPostにしかしないよ！
//            // パフォーマンスを最大限にしたい場合はuploadHandler, downloadHandlerをカスタマイズすること

//            // JSONでbodyに送るというパラメータで送るという雑設定。
//            var data = JsonUtility.ToJson(context.Value);
//            var formData = new Dictionary<string, string> { { "body", data } };

//            using (var req = UnityWebRequest.Post(basePath + context.Path, formData))
//            {
//                var header = context.GetRawHeaders();
//                if (header != null)
//                {
//                    foreach (var item in header)
//                    {
//                        req.SetRequestHeader(item.Key, item.Value);
//                    }
//                }

//                // Timeout処理はCancellationTokenSourceのCancelAfterSlim(UniTask拡張)を使ってサクッと処理
//                var linkToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
//                linkToken.CancelAfterSlim(timeout);
//                try
//                {
//                    // 完了待ちや終了処理はUniTaskの拡張自体に丸投げ
//                    await req.SendWebRequest().ToUniTask(progress: progress, cancellationToken: linkToken.Token);
//                }
//                catch (OperationCanceledException)
//                {
//                    // 元キャンセレーションソースがキャンセルしてなければTimeoutによるものと判定
//                    if (!cancellationToken.IsCancellationRequested)
//                    {
//                        throw new TimeoutException();
//                    }
//                }
//                finally
//                {
//                    // Timeoutに引っかからなかった場合にてるのでCancelAfterSlimの裏で回ってるループをこれで終わらせとく
//                    if (!linkToken.IsCancellationRequested)
//                    {
//                        linkToken.Cancel();
//                    }
//                }

//                // UnityWebRequestを先にDisposeしちゃうので先に必要なものを取得しておく（性能的には無駄なのでパフォーマンスを最大限にしたい場合は更に一工夫を）
//                return new ResponseContext(req.downloadHandler.data, req.responseCode, req.GetResponseHeaders());
//            }
//        }
//    }
//}