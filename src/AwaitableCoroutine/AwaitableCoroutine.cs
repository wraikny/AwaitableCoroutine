using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AwaitableCoroutine.Internal;

namespace AwaitableCoroutine
{
    public abstract class AwaitableCoroutineBase
    {
        internal protected ICoroutineRunner Runner { get; set; }
        protected internal Action OnCompleted { get; set; }

        public bool IsCompleted { get; protected internal set; } = false;

        internal protected abstract void _Pseudo();

        public AwaitableCoroutineBase()
        {
            Runner = ICoroutineRunner.GetContextStrict();
            Runner.Register(this);
        }

        internal AwaitableCoroutineBase(ICoroutineRunner runner)
        {
            Runner = runner;
            Runner.Register(this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ContinueWith(Action action)
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

            OnCompleted += action;
        }

        protected abstract void OnMoveNext();

        public void MoveNext()
        {
            if (IsCompleted) return;
            OnMoveNext();
        }

        public async AwaitableCoroutine With(Action action)
        {
            while (true)
            {
                if (IsCompleted) return;
                await AwaitableCoroutine.Yield();
            }
        }
    }

    [AsyncMethodBuilder(typeof(AwaitableCoroutineMethodBuilder))]
    public abstract partial class AwaitableCoroutine : AwaitableCoroutineBase
    {
        public CoroutineAwaiter GetAwaiter() => new CoroutineAwaiter(this);

        internal protected sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner): base(runner) { }

        protected void Complete()
        {
            IsCompleted = true;

            if (OnCompleted is null) return;
            Runner.Post(OnCompleted);
            OnCompleted = null;
            Runner = null;
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

        internal protected sealed override void _Pseudo() { }

        public AwaitableCoroutine() { }

        internal AwaitableCoroutine(ICoroutineRunner runner): base(runner) { }

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
