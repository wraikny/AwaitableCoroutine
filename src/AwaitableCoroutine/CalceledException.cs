using System;

namespace AwaitableCoroutine
{
    public class CanceledException : Exception
    {
        public CanceledException()
        {

        }

        public CanceledException(string message)
            : base(message)
        {

        }
    }

    public sealed class ChildCanceledException<T> : CanceledException
        where T : AwaitableCoroutineBase
    {
        public T Child { get; private set; }
        public ChildCanceledException(T child)
        {
            Child = child;
        }

        public ChildCanceledException(T child, string message)
            : base(message)
        {
            Child = child;
        }
    }

    public sealed class ChildrenCanceledException<T> : CanceledException
        where T : AwaitableCoroutineBase
    {
        public T[] Children { get; private set; }
        public ChildrenCanceledException(T[] children)
        {
            Children = children;
        }

        public ChildrenCanceledException(T[] children, string message)
            : base(message)
        {
            Children = children;
        }
    }

    public partial class AwaitableCoroutine
    {
        public static void ThrowCancel(string message = null)
        {
            throw new CanceledException(message);
        }

        public static void ThrowChildCancel<T>(T child, string message = null)
            where T : AwaitableCoroutineBase
        {
            throw new ChildCanceledException<T>(child, message);
        }

        public static void ThrowChildrenCancel<T>(T[] children, string message = null)
            where T : AwaitableCoroutineBase
        {
            throw new ChildrenCanceledException<T>(children, message);
        }
    }
}
