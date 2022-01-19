using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine.Internal
{
    public interface ICoroutineAwaiter : INotifyCompletion, ICriticalNotifyCompletion { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct CoroutineAwaiter : ICoroutineAwaiter, IWaitingCoroutineRegisterer
    {
        private readonly AwaitableCoroutine _target;

        public AwaitableCoroutine Target => _target;

        public CoroutineAwaiter(AwaitableCoroutine target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompletedSuccessfully;

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        public void GetResult() { }

        void IWaitingCoroutineRegisterer.RegisterWaitingCoroutine(AwaitableCoroutineBase child)
        {
            Target.RegisterWaitingCoroutine(child);
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct CoroutineAwaiter<T> : ICoroutineAwaiter, IWaitingCoroutineRegisterer
    {
        private readonly AwaitableCoroutine<T> _target;

        public AwaitableCoroutine<T> Target => _target;

        public CoroutineAwaiter(AwaitableCoroutine<T> target)
        {
            _target = target;
        }

        public bool IsCompleted => _target.IsCompletedSuccessfully;

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        void ICriticalNotifyCompletion.UnsafeOnCompleted(Action continuation)
        {
            _target.ContinueWith(continuation);
        }

        public T GetResult() => _target.Result;

        void IWaitingCoroutineRegisterer.RegisterWaitingCoroutine(AwaitableCoroutineBase child)
        {
            Target.RegisterWaitingCoroutine(child);
        }
    }
}
