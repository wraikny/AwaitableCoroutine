using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class CoroutineBase
    {
        internal event Action OnUpdating = null;
        protected internal ICoroutineRunner Runner { get; set; }
        internal Action OnCompleted { get; set; }

        public bool IsCompletedSuccessfully { get; protected internal set; } = false;

        public Exception Exception { get; private set; } = null;

        public bool IsCanceled { get; private set; } = false;
        private Action OnCalceled { get; set; }

        ///<summary>
        /// Get AwaitableCoroutine is completed successfully or canceled
        ///</summary>
        public bool IsCompleted => IsCompletedSuccessfully || IsCanceled;

        internal List<CoroutineBase> WaitingCoroutines { get; set; }

        protected internal abstract void _Pseudo();

        public CoroutineBase()
        {
            Runner = ICoroutineRunner.GetContext();
            Internal.Logger.Log($"{GetType()} is created");
            Runner.Register(this);
        }

        internal CoroutineBase(ICoroutineRunner runner)
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

            if (IsCompletedSuccessfully)
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

            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed successfully");
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

            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed successfully");
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
                    if (child.IsCompleted) continue;
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

        internal void RegisterWaitingCoroutine(CoroutineBase coroutine)
        {
            if (IsCanceled)
            {
                if (coroutine.IsCompleted) return;
                coroutine.Cancel();
                return;
            }

            WaitingCoroutines ??= new List<CoroutineBase>();
            WaitingCoroutines.Add(coroutine);
        }

        public void MoveNext()
        {
            if (IsCanceled)
            {
                ThrowHelper.InvalidOp("Coroutine is alread canceled");
            }

            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine is alread completed successfully");
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
    public abstract partial class Coroutine : CoroutineBase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        protected internal sealed override void _Pseudo() { }

        public Coroutine() { }

        internal Coroutine(ICoroutineRunner runner) : base(runner) { }

        protected void Complete()
        {
            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine already completed successfully");
            }

            if (IsCanceled)
            {
                ThrowHelper.InvalidOp($"Coroutine is already canceled");
            }

            IsCompletedSuccessfully = true;
            OnCompleted?.Invoke();
            OnCompleted = null;
            Runner = null;
            WaitingCoroutines = null;
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class Coroutine
        <T> : CoroutineBase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        protected internal sealed override void _Pseudo() { }

        public Coroutine() { }

        internal Coroutine(ICoroutineRunner runner) : base(runner) { }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine already completed successfully");
            }

            if (IsCanceled)
            {
                ThrowHelper.InvalidOp($"Coroutine is already canceled");
            }

            IsCompletedSuccessfully = true;
            Result = result;

            OnCompleted?.Invoke();
            OnCompleted = null;
            Runner = null;
            WaitingCoroutines = null;
        }
    }
}
