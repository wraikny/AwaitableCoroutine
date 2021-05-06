﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class AwaitableCoroutineBase
    {
        protected internal ICoroutineRunner Runner { get; private set; }
        protected internal Action OnCompleted { get; private set; }

        public bool IsCompleted { get; protected internal set; }

        public AwaitableCoroutineBase()
        {
            OnCompleted = null;
            IsCompleted = false;

            var runner = ICoroutineRunner.Context;

            if (runner is null)
            {
                throw new InvalidOperationException("Out of ICoroutineRunner context");
            }

            runner.OnRegistering(this);
            Runner = runner;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ContinueWith(Action action)
        {
            if (action is null) return;
            OnCompleted = action;
        }

        public abstract void MoveNext();
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract partial class AwaitableCoroutine : AwaitableCoroutineBase
    {
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        public AwaitableCoroutine()
        {

        }

        protected void Complete()
        {
            if (Runner is null)
            {
                throw new InvalidOperationException("Coroutine not registered");
            }

            if (IsCompleted)
            {
                throw new InvalidOperationException("Coroutine already completed");
            }

            IsCompleted = true;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class AwaitableCoroutine<T> : AwaitableCoroutineBase
    {
        public CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        public AwaitableCoroutine()
        {

        }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (Runner is null)
            {
                throw new InvalidOperationException($"Coroutine not registered");
            }

            if (IsCompleted)
            {
                throw new InvalidOperationException("Coroutine already completed");
            }

            IsCompleted = true;
            Result = result;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
        }
    }
}
