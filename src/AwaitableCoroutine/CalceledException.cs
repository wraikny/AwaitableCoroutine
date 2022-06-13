using System;

namespace AwaitableCoroutine
{
    public class CanceledException : Exception
    {
        public Coroutine Coroutine { get; internal set; }

        public CanceledException(string message = null, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }

    public sealed class ChildCanceledException : CanceledException
    {
        public Coroutine Child { get; private set; }
        public ChildCanceledException(Coroutine child, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Child = child;
        }
    }

    public sealed class ChildrenCanceledException : CanceledException
    {
        public Coroutine[] Children { get; private set; }
        public ChildrenCanceledException(Coroutine[] children, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            Children = children;
        }
    }

    public partial class Coroutine
    {
        public static void ThrowCancel(string message = null, Exception innerException = null, Coroutine coroutine = null)
        {
            throw new CanceledException(message, innerException)
            {
                Coroutine = coroutine,
            };
        }

        public static void ThrowChildCancel(Coroutine child, string message = null, Exception innerException = null, Coroutine coroutine = null)
        {
            throw new ChildCanceledException(child, message, innerException)
            {
                Coroutine = coroutine,
            };
        }

        public static void ThrowChildrenCancel(Coroutine[] children, string message = null, Exception innerException = null, Coroutine coroutine = null)
        {
            throw new ChildrenCanceledException(children, message, innerException)
            {
                Coroutine = coroutine,
            };
        }
    }
}
