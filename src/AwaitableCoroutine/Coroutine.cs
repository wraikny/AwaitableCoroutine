using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public enum CoroutineStatus
    {
        Created,
        Running,
        Canceled,
        Faulted,
        RanToCompletion,
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract partial class Coroutine
    {
        internal event Action OnUpdating = null;
        protected internal ICoroutineRunner Runner { get; set; }

        public CoroutineStatus Status { get; internal set; }

        internal Action OnCompletedSuccessfully { get; set; }
        private Action OnCalceled { get; set; }

        public Exception Exception { get; private set; } = null;


        public bool IsCanceled => Status == CoroutineStatus.Canceled;
        public bool IsFaulted => Status == CoroutineStatus.Faulted;
        public bool IsCompletedSuccessfully => Status == CoroutineStatus.RanToCompletion;
        public bool IsCompleted => IsCanceled || IsFaulted || IsCompletedSuccessfully;

        public bool IsCanceledOrFaulted => IsCanceled || IsFaulted;

        public Coroutine()
        {
            Status = CoroutineStatus.Created;
            Runner = ICoroutineRunner.GetContext();
            Internal.Logger.Log($"{GetType()} is created");
            Runner.Register(this);
        }

        internal Coroutine(ICoroutineRunner runner)
        {
            Status = CoroutineStatus.Created;
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

            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine has already been completed");
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

            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine has already been completed");
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
            if (IsCompleted)
            {
                ThrowHelper.InvalidOp("Coroutine has already been completed");
            }

            SetException(new CanceledException("This coroutine has been canceled"));
        }

        internal void SetException(Exception exn)
        {
            if (exn is null)
            {
                ThrowHelper.ArgNull(nameof(exn));
            }

            if (exn is CanceledException e)
            {
                if (e.Coroutine is null)
                {
                    e.Coroutine = this;
                }

                Status = CoroutineStatus.Canceled;
            }
            else
            {
                Status = CoroutineStatus.Faulted;
            }

            Exception = exn;
            OnCalceled?.Invoke();
            OnCalceled = null;
            OnCompletedSuccessfully = null;
            Runner = null;
        }

        public void MoveNext()
        {
            if (Status == CoroutineStatus.Created)
            {
                Status = CoroutineStatus.Running;
            }
            else if (Status != CoroutineStatus.Running)
            {
                ThrowHelper.InvalidOp("CoroutineStatus is invalid.");
            }

            try
            {
                Internal.Logger.Log($"{GetType()} move next");
                OnUpdating?.Invoke();
                OnMoveNext();
            }
            catch (CanceledException e)
            {
                if (e.Coroutine is Coroutine c && c != this)
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        protected void Complete()
        {
            if (IsCompleted)
            {
                ThrowHelper.InvalidOp($"Coroutine has already been completed");
            }

            Status = CoroutineStatus.RanToCompletion;

            OnCompletedSuccessfully?.Invoke();
            OnCompletedSuccessfully = null;

            Runner = null;
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder<>))]
    public abstract class Coroutine
        <T> : Coroutine
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new CoroutineAwaiter<T> GetAwaiter() => new CoroutineAwaiter<T>(this);

        public Coroutine() { }

        internal Coroutine(ICoroutineRunner runner) : base(runner) { }

        public T Result { get; private set; }

        protected void Complete(T result)
        {
            if (IsCompleted)
            {
                ThrowHelper.InvalidOp($"Coroutine has already been completed");
            }

            Status = CoroutineStatus.RanToCompletion;
            Result = result;

            OnCompletedSuccessfully?.Invoke();
            OnCompletedSuccessfully = null;

            Runner = null;
        }
    }
}
