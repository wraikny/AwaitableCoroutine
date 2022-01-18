﻿using System;
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
                var runner = ICoroutineRunner.GetContext();
                runner.Post(continuation);
            }

            public void GetResult() { }
        }
    }
}
