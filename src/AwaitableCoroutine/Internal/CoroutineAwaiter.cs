using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace AwaitableCoroutine.Internal
{
    public interface ICoroutineAwaiter : INotifyCompletion, ICriticalNotifyCompletion { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct CoroutineAwaiter : ICoroutineAwaiter
    {
        private readonly Coroutine _target;

        public Coroutine Target => _target;

        public CoroutineAwaiter(Coroutine target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompleted;

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (_target.IsCompleted)
            {
                continuation.Invoke();
                return;
            }

            _target.AddOnCompletedSuccessfully(continuation);
            _target.AddOnCanceled(continuation);
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        {
            if (_target.IsCompleted)
            {
                continuation.Invoke();
                return;
            }

            _target.AddOnCompletedSuccessfully(continuation);
            _target.AddOnCanceled(continuation);
        }

        public void GetResult()
        {
            if (_target.IsCanceled)
            {
                if (_target.Exception is CanceledException e)
                {
                    ExceptionDispatchInfo.Capture(e).Throw();
                }
                else
                {
                    Coroutine.ThrowCancel(innerException: _target.Exception);
                }
            }
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct CoroutineAwaiter<T> : ICoroutineAwaiter
    {
        private readonly Coroutine<T> _target;

        public Coroutine<T> Target => _target;

        public CoroutineAwaiter(Coroutine<T> target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompleted;

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (_target.IsCompleted)
            {
                continuation.Invoke();
                return;
            }

            _target.AddOnCompletedSuccessfully(continuation);
            _target.AddOnCanceled(continuation);
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        {
            if (_target.IsCompleted)
            {
                continuation.Invoke();
                return;
            }

            _target.AddOnCompletedSuccessfully(continuation);
            _target.AddOnCanceled(continuation);
        }

        public T GetResult()
        {
            if (_target.IsCanceled)
            {
                if (_target.Exception is CanceledException e)
                {
                    ExceptionDispatchInfo.Capture(e).Throw();
                }
                else
                {
                    Coroutine.ThrowCancel(innerException: _target.Exception);
                }
            }

            return _target.Result;
        }
    }
}
