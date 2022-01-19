using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine.Internal
{

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct YieldAwaitable
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public YieldAwaiter GetAwaiter() => new YieldAwaiter();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly struct YieldAwaiter : ICoroutineAwaiter
        {
            public bool IsCompleted
            {
                get
                {
                    Internal.Logger.Log("YieldAwaiter.IsCompleted");
                    return false;
                }
            }

            private static void OnCompleted(Action continuation)
            {
                Internal.Logger.Log("YieldAwaiter.OnCompleted");
                var runner = ICoroutineRunner.GetContext();
                runner.Post(continuation);
            }

            void INotifyCompletion.OnCompleted(Action continuation)
            {
                YieldAwaiter.OnCompleted(continuation);
            }

            void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
            {
                YieldAwaiter.OnCompleted(continuation);
            }

            public void GetResult() { }
        }
    }
}
