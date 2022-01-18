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
        protected internal ICoroutineRunner Runner { get; set; }
        internal Action OnCompleted { get; set; }

        public bool IsCompleted { get; protected internal set; } = false;

        public Exception Exception { get; private set; } = null;

        public bool IsCanceled { get; private set; } = false;
        private Action OnCalceled { get; set; }

        internal List<AwaitableCoroutineBase> WaitingCoroutines { get; set; }

        protected internal abstract void _Pseudo();

        public AwaitableCoroutineBase()
        {
            Runner = ICoroutineRunner.GetContext();
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
                ThrowHelper.ArgNull(nameof(action));
            }

            if (IsCanceled)
            {
                ThrowHelper.InvalidOp("Coroutine is already canceled");
            }

            if (IsCompleted)
            {
                action.Invoke();
                return;
            }

            var runner = ICoroutineRunner.Instance;

            if (runner is null || Runner == runner)
            {
                OnCompleted += action;
            }
            else
            {
                OnCompleted += () => runner.Context(action);
            }
        }

        protected abstract void OnMoveNext();

        internal void AddOnCanceled(Action onCanceled)
        {
            if (onCanceled is null)
            {
                ThrowHelper.ArgNull(nameof(onCanceled));
            }

            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed");
            }

            if (IsCanceled)
            {
                onCanceled.Invoke();
                return;
            }

            var runner = ICoroutineRunner.Instance;

            if (runner is null || Runner == runner)
            {
                OnCalceled += onCanceled;
            }
            else
            {
                OnCalceled += () => runner.Context(onCanceled);
            }
        }

        public void Cancel()
        {
            if (IsCanceled)
            {
                ThrowHelper.InvalidOp("Coroutien is already canceled");
            }

            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed");
            }

            IsCanceled = true;
            OnCalceled?.Invoke();
            OnCalceled = null;
            OnCompleted = null;
            Runner = null;

            if (WaitingCoroutines is { })
            {
                foreach (var child in WaitingCoroutines)
                {
                    if (child.IsCanceled || child.IsCompleted) continue;
                    child.Cancel();
                }
            }
        }

        internal void SetException(Exception exn)
        {
            if (exn is null)
            {
                ThrowHelper.ArgNull(nameof(exn));
            }

            Exception = exn;
            Cancel();
        }

        internal void RegisterWaitingCoroutine(AwaitableCoroutineBase coroutine)
        {
            if (IsCanceled)
            {
                if (coroutine.IsCanceled || coroutine.IsCompleted) return;
                coroutine.Cancel();
                return;
            }

            WaitingCoroutines ??= new List<AwaitableCoroutineBase>();
            WaitingCoroutines.Add(coroutine);
        }

        public void MoveNext()
        {
            if (IsCanceled)
            {
                ThrowHelper.InvalidOp("Coroutine is alread canceled");
            }

            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine is alread completed");
            }

            try
            {
                Internal.Logger.Log($"{GetType()} move next");
                OnUpdating?.Invoke();
                OnMoveNext();
            }
            catch (Exception exn)
            {
                Exception = exn;
            }
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract partial class AwaitableCoroutine : AwaitableCoroutineBase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        protected internal sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner) : base(runner) { }

        protected void Complete()
        {
            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine already completed");
            }

            if (IsCanceled)
            {
                ThrowHelper.InvalidOp($"Coroutine is already canceled");
            }

            IsCompleted = true;
            OnCompleted?.Invoke();
            OnCompleted = null;
            Runner = null;
            WaitingCoroutines = null;
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class AwaitableCoroutine<T> : AwaitableCoroutineBase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        protected internal sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner) : base(runner) { }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine already completed");
            }

            if (IsCanceled)
            {
                ThrowHelper.InvalidOp($"Coroutine is already canceled");
            }

            IsCompleted = true;
            Result = result;

            OnCompleted?.Invoke();
            OnCompleted = null;
            Runner = null;
            WaitingCoroutines = null;
        }
    }
}
