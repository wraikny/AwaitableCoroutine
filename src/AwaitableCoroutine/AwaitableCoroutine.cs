using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class AwaitableCoroutineBase
    {
        internal ICoroutineRunner Runner { get; private set; }
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

        protected abstract void OnMoveNext();

        public void MoveNext()
        {
            if (IsCompleted) return;
            OnMoveNext();
        }
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

        public async AwaitableCoroutine AndThen(Action thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await this;
            thunk();
        }

        public async AwaitableCoroutine<U> AndThen<U>(Func<U> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await this;
            return thunk();
        }

        public async AwaitableCoroutine AndThen(Func<AwaitableCoroutine> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await this;
            await thunk();
        }

        public async AwaitableCoroutine<T> AndThen<T>(Func<AwaitableCoroutine<T>> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            await this;
            return await thunk();
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

        public async AwaitableCoroutine AndThen(Action<T> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await this;
            thunk(res);
        }

        public async AwaitableCoroutine<U> AndThen<U>(Func<T, U> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await this;
            return thunk(res);
        }

        public async AwaitableCoroutine AndThen<U>(Func<T, AwaitableCoroutine> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await this;
            await thunk(res);
        }

        public async AwaitableCoroutine<U> AndThen<U>(Func<T, AwaitableCoroutine<U>> thunk)
        {
            if (thunk is null)
            {
                throw new ArgumentNullException(nameof(thunk));
            }

            var res = await this;
            return await thunk(res);
        }
    }
}
