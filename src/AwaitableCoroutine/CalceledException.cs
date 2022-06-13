using System;

namespace AwaitableCoroutine
{
    public class CanceledException : Exception
    {
        public CoroutineBase Coroutine { get; internal set; }

        public CanceledException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }

    public sealed class ChildCanceledException : CanceledException
    {
        public CoroutineBase Child { get; private set; }
        public ChildCanceledException(CoroutineBase child, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Child = child;
        }
    }

    public sealed class ChildrenCanceledException : CanceledException
    {
        public CoroutineBase[] Children { get; private set; }
        public ChildrenCanceledException(CoroutineBase[] children, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Children = children;
        }
    }

    public partial class Coroutine
    {
        public static void ThrowCancel(string message = null, Exception innerException = null)
        {
            throw new CanceledException(message, innerException);
        }

        public static void ThrowChildCancel(CoroutineBase child, string message = null, Exception innerException = null)
        {
            throw new ChildCanceledException(child, message, innerException);
        }

        public static void ThrowChildrenCancel(CoroutineBase[] children, string message = null, Exception innerException = null)
        {
            throw new ChildrenCanceledException(children, message, innerException);
        }
    }
}
