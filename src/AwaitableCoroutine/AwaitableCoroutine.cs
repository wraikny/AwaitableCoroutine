using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class AwaitableCoroutineBase
    {
        internal event Action OnUpdating = null;
        internal protected ICoroutineRunner Runner { get; set; }
        protected internal Action OnCompleted { get; set; }

        public bool IsCompleted { get; protected internal set; } = false;

        internal protected abstract void _Pseudo();

        public AwaitableCoroutineBase()
        {
            Runner = ICoroutineRunner.GetContextStrict();
            Internal.Logger.Log($"{GetType()} is created");
            Runner.Register(this);
        }

        internal AwaitableCoroutineBase(ICoroutineRunner runner)
        {
            Runner = runner;
            Internal.Logger.Log($"{GetType()} is created");
            Runner.Register(this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void ContinueWith(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (IsCompleted)
            {
                action.Invoke();
                return;
            }

            var runner = ICoroutineRunner.GetContextStrict();

            if (Runner == runner)
            {
                OnCompleted += action;
            }
            else
            {
                OnCompleted += () => runner.Context(action);
            }
        }

        protected abstract void OnMoveNext();

        public void MoveNext()
        {
            if (IsCompleted) return;
            Internal.Logger.Log($"{GetType()} move next");
            OnUpdating?.Invoke();
            OnMoveNext();
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract partial class AwaitableCoroutine : AwaitableCoroutineBase
    {
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        internal protected sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner) : base(runner) { }

        protected void Complete()
        {
            IsCompleted = true;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
            OnCompleted = null;
            Runner = null;
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class AwaitableCoroutine<T> : AwaitableCoroutineBase
    {
        public CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        internal protected sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner) : base(runner) { }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (IsCompleted)
            {
                throw new InvalidOperationException("Coroutine already completed");
            }

            IsCompleted = true;
            Result = result;

            if (OnCompleted is null) return;

            Runner.Post(OnCompleted);
            OnCompleted = null;
            Runner = null;
        }
    }
}
