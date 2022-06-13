using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class CoroutineBase
    {
        internal event Action OnUpdating = null;
        protected internal ICoroutineRunner Runner { get; set; }
        internal Action OnCompletedSuccessfully { get; set; }

        public bool IsCompletedSuccessfully { get; protected internal set; } = false;

        public Exception Exception { get; private set; } = null;

        public bool IsCanceled { get; private set; } = false;
        private Action OnCalceled { get; set; }

        ///<summary>
        /// Get AwaitableCoroutine is completed successfully or canceled
        ///</summary>
        public bool IsCompleted => IsCompletedSuccessfully || IsCanceled;

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

        protected abstract void OnMoveNext();

        internal void AddOnCanceled(Action action)
        {
            if (action is null)
            {
                ThrowHelper.ArgNull(nameof(action));
            }

            if (IsCompletedSuccessfully)
            {
                ThrowHelper.InvalidOp("Coroutine is already completed successfully");
            }

            if (IsCanceled)
            {
                action.Invoke();
                return;
            }

            var runner = ICoroutineRunner.Instance;

            if (runner is null || Runner == runner)
            {
                OnCalceled += action;
            }
            else
            {
                OnCalceled += () => runner.Context(action);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void AddOnCompletedSuccessfully(Action action)
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
                OnCompletedSuccessfully += action;
            }
            else
            {
                OnCompletedSuccessfully += () => runner.Context(action);
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

            SetException(new CanceledException("Canceled"));
        }

        internal void SetException(Exception exn)
        {
            if (exn is null)
            {
                ThrowHelper.ArgNull(nameof(exn));
            }

            if (exn is CanceledException e)
            {
                e.Coroutine = this;
            }

            Exception = exn;
            IsCanceled = true;
            OnCalceled?.Invoke();
            OnCalceled = null;
            OnCompletedSuccessfully = null;
            Runner = null;
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
            catch (CanceledException e)
            {
                if (e.Coroutine is CoroutineBase c && c != this)
                {
                    SetException(new ChildCanceledException(c, e.Message, e));
                }
                else
                {
                    SetException(e);
                }
            }
            catch (Exception exn)
            {
                SetException(exn);
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

            OnCompletedSuccessfully?.Invoke();
            OnCompletedSuccessfully = null;

            Runner = null;
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

            OnCompletedSuccessfully?.Invoke();
            OnCompletedSuccessfully = null;

            Runner = null;
        }
    }
}
