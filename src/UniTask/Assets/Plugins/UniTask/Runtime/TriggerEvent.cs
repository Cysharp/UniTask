using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public interface ITriggerHandler<T>
    {
        void OnNext(T value);
        void OnError(Exception ex);
        void OnCompleted();
        void OnCanceled(CancellationToken cancellationToken);

        // set/get from TriggerEvent<T>
        ITriggerHandler<T> Prev { get; set; }
        ITriggerHandler<T> Next { get; set; }
    }

    // be careful to use, itself is struct.
    public struct TriggerEvent<T>
    {
        ITriggerHandler<T> head; // head.prev is last
        ITriggerHandler<T> iteratingHead;
        ITriggerHandler<T> iteratingNode;

        void LogError(Exception ex)
        {
#if UNITY_2018_3_OR_NEWER
            UnityEngine.Debug.LogException(ex);
#else
            Console.WriteLine(ex);
#endif
        }

        public void SetResult(T value)
        {
            if (iteratingNode != null)
            {
                throw new InvalidOperationException("Can not trigger itself in iterating.");
            }

            var h = head;
            while (h != null)
            {
                iteratingNode = h;

                try
                {
                    h.OnNext(value);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    Remove(h);
                }

                // If `h` itself is removed by OnNext, h.Next is null.
                // Therefore, instead of looking at h.Next, the `iteratingNode` reference itself is replaced.
                h = h == iteratingNode ? h.Next : iteratingNode;
            }

            iteratingNode = null;
            if (iteratingHead != null)
            {
                Add(iteratingHead);
                iteratingHead = null;
            }
        }

        public void SetCanceled(CancellationToken cancellationToken)
        {
            if (iteratingNode != null)
            {
                throw new InvalidOperationException("Can not trigger itself in iterating.");
            }

            var h = head;
            while (h != null)
            {
                iteratingNode = h;
                try
                {
                    h.OnCanceled(cancellationToken);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                var next = h == iteratingNode ? h.Next : iteratingNode;
                iteratingNode = null;
                Remove(h);
                h = next;
            }

            iteratingNode = null;
            if (iteratingHead != null)
            {
                Add(iteratingHead);
                iteratingHead = null;
            }
        }

        public void SetCompleted()
        {
            if (iteratingNode != null)
            {
                throw new InvalidOperationException("Can not trigger itself in iterating.");
            }

            var h = head;
            while (h != null)
            {
                iteratingNode = h;
                try
                {
                    h.OnCompleted();
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                var next = h == iteratingNode ? h.Next : iteratingNode;
                iteratingNode = null;
                Remove(h);
                h = next;
            }

            iteratingNode = null;
            if (iteratingHead != null)
            {
                Add(iteratingHead);
                iteratingHead = null;
            }
        }

        public void SetError(Exception exception)
        {
            if (iteratingNode != null)
            {
                throw new InvalidOperationException("Can not trigger itself in iterating.");
            }

            var h = head;
            while (h != null)
            {
                iteratingNode = h;
                try
                {
                    h.OnError(exception);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                var next = h == iteratingNode ? h.Next : iteratingNode;
                iteratingNode = null;
                Remove(h);
                h = next;
            }

            iteratingNode = null;
            if (iteratingHead != null)
            {
                Add(iteratingHead);
                iteratingHead = null;
            }
        }

        public void Add(ITriggerHandler<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            // zero node.
            if (head == null)
            {
                head = handler;
                return;
            }

            if (iteratingNode != null)
            {
                if (iteratingHead == null)
                {
                    iteratingHead = handler;
                    return;
                }

                var last = iteratingHead.Prev;
                if (last == null)
                {
                    // single node.
                    iteratingHead.Prev = handler;
                    iteratingHead.Next = handler;
                    handler.Prev = iteratingHead;
                }
                else
                {
                    // multi node
                    iteratingHead.Prev = handler;
                    last.Next = handler;
                    handler.Prev = last;
                }
            }
            else
            {
                var last = head.Prev;
                if (last == null)
                {
                    // single node.
                    head.Prev = handler;
                    head.Next = handler;
                    handler.Prev = head;
                }
                else
                {
                    // multi node
                    head.Prev = handler;
                    last.Next = handler;
                    handler.Prev = last;
                }
            }
        }

        public void Remove(ITriggerHandler<T> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var prev = handler.Prev;
            var next = handler.Next;

            if (next != null)
            {
                next.Prev = prev;
            }

            if (handler == head)
            {
                head = next;
            }
            // when handler is head, prev indicate last so don't use it.
            else if (prev != null)
            {
                prev.Next = next;
            }

            if (handler == iteratingNode)
            {
                iteratingNode = next;
            }
            if (handler == iteratingHead)
            {
                iteratingHead = next;
            }

            if (head != null)
            {
                if (head.Prev == handler)
                {
                    if (prev != head)
                    {
                        head.Prev = prev;
                    }
                    else
                    {
                        head.Prev = null;
                    }
                }
            }

            if (iteratingHead != null)
            {
                if (iteratingHead.Prev == handler)
                {
                    if (prev != iteratingHead.Prev)
                    {
                        iteratingHead.Prev = prev;
                    }
                    else
                    {
                        iteratingHead.Prev = null;
                    }
                }
            }

            handler.Prev = null;
            handler.Next = null;
        }
    }
}
