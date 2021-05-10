using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine
{

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct YieldAwaitable
    {
        public YieldAwaiter GetAwaiter() => new YieldAwaiter();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly struct YieldAwaiter : INotifyCompletion
        {
            public bool IsCompleted
            {
                get
                {
                    Internal.Logger.Log("YieldAwaiter.IsCompleted");
                    return false;
                }
            }

            public void OnCompleted(Action continuation)
            {
                Internal.Logger.Log("YieldAwaiter.OnCompleted");
                var runner = ICoroutineRunner.GetContextStrict();
                runner.Post(continuation);
            }

            public void GetResult() { }
        }
    }

    public partial class AwaitableCoroutine
    {
        public static YieldAwaitable Yield()
        {
            Internal.Logger.Log("AwaitableCoroutine.Yield()");
            return new YieldAwaitable();
        }
    }
}
