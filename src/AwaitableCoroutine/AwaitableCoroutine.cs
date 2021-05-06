using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class AwaitableCoroutineBase
    {
        protected internal ICoroutineRunner Runner { get; private set; }
        protected internal Action OnCompleted { get; private set; }

        public bool IsRegistered { get; private set; }

        public AwaitableCoroutineBase(ICoroutineRunner runner)
        {
            if (runner is null)
            {
                throw new ArgumentNullException(nameof(runner));
            }

            Runner = runner;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ContinueWith(Action action)
        {
            if (action is null) return;
            OnCompleted = action;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetRegistered()
        {
            IsRegistered = true;
        }

        public bool IsCompleted { get; protected internal set; }
        public abstract void MoveNext();
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract class AwaitableCoroutine : AwaitableCoroutineBase
    {
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        public AwaitableCoroutine(ICoroutineRunner runner)
            : base(runner)
        {

        }

        protected void Complete()
        {
            if (IsCompleted) return;
            IsCompleted = true;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class AwaitableCoroutine<T> : AwaitableCoroutineBase
    {
        public CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        public AwaitableCoroutine(ICoroutineRunner runner)
            : base(runner)
        {

        }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (IsCompleted) return;
            IsCompleted = true;
            Result = result;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
        }
    }
}
