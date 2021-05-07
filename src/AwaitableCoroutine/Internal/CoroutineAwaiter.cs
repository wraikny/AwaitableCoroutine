﻿using System;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine.Internal
{
    public readonly struct CoroutineAwaiter : INotifyCompletion
    {
        private readonly AwaitableCoroutine _target;

        public CoroutineAwaiter(AwaitableCoroutine target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompleted;

        public void OnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        public void GetResult() { }
    }

    public readonly struct CoroutineAwaiter<T> : INotifyCompletion
    {
        private readonly AwaitableCoroutine<T> _target;

        public CoroutineAwaiter(AwaitableCoroutine<T> target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompleted;

        public void OnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        public T GetResult() => _target.Result;
    }
}
