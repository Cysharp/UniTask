using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

[Config(typeof(BenchmarkConfig))]
public class QueueCheck
{
    Node node1 = new Node();
    Node node2 = new Node();
    RefNode refNode1 = new RefNode();
    RefNode refNode2 = new RefNode();
    Queue<Node> q1 = new Queue<Node>();
    Stack<Node> s1 = new Stack<Node>();
    ConcurrentQueue<Node> cq = new ConcurrentQueue<Node>();
    ConcurrentStack<Node> cs = new ConcurrentStack<Node>();
    static TaskPool<Node> pool;
    static TaskPoolRefNode<RefNode> poolRefNode;
    static TaskPoolEqualNull<Node> poolEqualNull;
    static TaskPoolClass<Node> poolClass = new TaskPoolClass<Node>();
    static TaskPoolWithoutSize<Node> poolWithoutSize;
    static TaskPoolWithoutLock<Node> poolWithoutLock;

    [Benchmark]
    public void Queue()
    {
        q1.Enqueue(node1);
        q1.Enqueue(node1);
        q1.TryDequeue(out _);
        q1.TryDequeue(out _);
    }

    [Benchmark]
    public void QueueLock()
    {
        lock (q1) { q1.Enqueue(node1); }
        lock (q1) { q1.Enqueue(node1); }
        lock (q1) { q1.TryDequeue(out _); }
        lock (q1) { q1.TryDequeue(out _); }
    }

    [Benchmark]
    public void Stack()
    {
        s1.Push(node1);
        s1.Push(node2);
        s1.TryPop(out _);
        s1.TryPop(out _);
    }

    [Benchmark]
    public void StackLock()
    {
        lock (s1) { s1.Push(node1); }
        lock (s1) { s1.Push(node2); }
        lock (s1) { s1.TryPop(out _); }
        lock (s1) { s1.TryPop(out _); }
    }

    [Benchmark]
    public void ConcurrentQueue()
    {
        cq.Enqueue(node1);
        cq.Enqueue(node1);
        cq.TryDequeue(out _);
        cq.TryDequeue(out _);
    }

    [Benchmark]
    public void ConcurrentStack()
    {
        cs.Push(node1);
        cs.Push(node2);
        cs.TryPop(out _);
        cs.TryPop(out _);
    }

    [Benchmark]
    public void TaskPool()
    {
        pool.TryPush(node1);
        pool.TryPush(node2);
        pool.TryPop(out _);
        pool.TryPop(out _);
    }
    [Benchmark]
    public void TaskPoolRefNode()
    {
        poolRefNode.TryPush(refNode1);
        poolRefNode.TryPush(refNode2);
        poolRefNode.TryPop(out _);
        poolRefNode.TryPop(out _);
    }

    [Benchmark]
    public void TaskPoolEqualNull()
    {
        poolEqualNull.TryPush(node1);
        poolEqualNull.TryPush(node2);
        poolEqualNull.TryPop(out _);
        poolEqualNull.TryPop(out _);
    }

    [Benchmark]
    public void TaskPoolClass()
    {
        poolClass.TryPush(node1);
        poolClass.TryPush(node2);
        poolClass.TryPop(out _);
        poolClass.TryPop(out _);
    }

    [Benchmark]
    public void TaskPoolWithoutSize()
    {
        poolWithoutSize.TryPush(node1);
        poolWithoutSize.TryPush(node2);
        poolWithoutSize.TryPop(out _);
        poolWithoutSize.TryPop(out _);
    }

    [Benchmark]
    public void TaskPoolWithoutLock()
    {
        poolWithoutLock.TryPush(node1);
        poolWithoutLock.TryPush(node2);
        poolWithoutLock.TryPop(out _);
        poolWithoutLock.TryPop(out _);
    }
}

public sealed class Node : ITaskPoolNode<Node>
{
    public Node NextNode { get; set; }
}

public interface ITaskPoolNode<T>
{
    T NextNode { get; set; }
}

public sealed class RefNode :ITaskPoolRefNode<RefNode>
{
    RefNode nextNode;
    public ref RefNode NextNode => ref nextNode;
}

public interface ITaskPoolRefNode<T>
{
    ref T NextNode { get; }
}


// mutable struct, don't mark readonly.
[StructLayout(LayoutKind.Auto)]
public struct TaskPoolWithoutLock<T>
    where T : class, ITaskPoolNode<T>
{
    int size;
    T root;

    public int Size => size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        //if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (!(v is null))
            {
                root = v.NextNode;
                v.NextNode = null;
                size--;
                result = v;
                //      Volatile.Write(ref gate, 0);
                return true;
            }

            //Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        //if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                size++;
                //      Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        //return false;
    }
}

[StructLayout(LayoutKind.Auto)]
public struct TaskPool<T>
    where T : class, ITaskPoolNode<T>
{
    int gate;
    int size;
    T root;

    public int Size => size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (!(v is null))
            {
                root = v.NextNode;
                v.NextNode = null;
                size--;
                result = v;
                Volatile.Write(ref gate, 0);
                return true;
            }

            Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                size++;
                Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        return false;
    }
}
[StructLayout(LayoutKind.Auto)]
public struct TaskPoolRefNode<T>
    where T : class, ITaskPoolRefNode<T>
{
    int gate;
    int size;
    T root;

    public int Size => size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (!(v is null))
            {
                ref var nextNode = ref v.NextNode;
                root = nextNode;
                nextNode = null;
                size--;
                result = v;
                Volatile.Write(ref gate, 0);
                return true;
            }

            Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                size++;
                Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        return false;
    }
}

[StructLayout(LayoutKind.Auto)]
public struct TaskPoolEqualNull<T>
    where T : class, ITaskPoolNode<T>
{
    int gate;
    int size;
    T root;

    public int Size => size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (v != null)
            {
                root = v.NextNode;
                v.NextNode = null;
                size--;
                result = v;
                Volatile.Write(ref gate, 0);
                return true;
            }

            Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                size++;
                Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        return false;
    }
}

public class TaskPoolClass<T>
    where T : class, ITaskPoolNode<T>
{
    int gate;
    int size;
    T root;

    public int Size => size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (!(v is null))
            {
                root = v.NextNode;
                v.NextNode = null;
                size--;
                result = v;
                Volatile.Write(ref gate, 0);
                return true;
            }

            Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                size++;
                Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        return false;
    }
}

[StructLayout(LayoutKind.Auto)]
public struct TaskPoolWithoutSize<T>
    where T : class, ITaskPoolNode<T>
{
    int gate;
    T root;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPop(out T result)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            var v = root;
            if (!(v is null))
            {
                root = v.NextNode;
                v.NextNode = null;
                result = v;
                Volatile.Write(ref gate, 0);
                return true;
            }

            Volatile.Write(ref gate, 0);
        }
        result = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryPush(T item)
    {
        if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
        {
            //if (size < TaskPool.MaxPoolSize)
            {
                item.NextNode = root;
                root = item;
                Volatile.Write(ref gate, 0);
                return true;
            }
            //else
            {
                //  Volatile.Write(ref gate, 0);
            }
        }
        return false;
    }
}