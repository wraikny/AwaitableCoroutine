using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct AwaitableCoroutineMethodBuilder
    {
        private sealed class Coroutine : AwaitableCoroutine
        {
            public Coroutine(ICoroutineRunner runner) : base(runner) { }

            public void CallComplete() => Complete();

            protected override void OnMoveNext() { }
        }

        private readonly ICoroutineRunner _runner;
        private readonly Coroutine _coroutine;

        private AwaitableCoroutineMethodBuilder(ICoroutineRunner runner)
        {
            Logger.Log("AwaitableCoroutineMethodBuilder constructor");
            _runner = runner;
            _coroutine = new Coroutine(runner);
        }

        // 1. Static Create method.
        public static AwaitableCoroutineMethodBuilder Create()
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.Create");
            return new AwaitableCoroutineMethodBuilder(ICoroutineRunner.GetContext());
        }

        // 2. TaskLike Task Property
        public AwaitableCoroutine Task => _coroutine;

        // 3. SetException
        public void SetException(Exception exn)
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.SetException");
            _coroutine.SetException(exn);
        }

        // 4. SetResult
        public void SetResult()
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.SetResult");
            _coroutine.CallComplete();
        }

        // 5. AwaitOnCompleted
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICoroutineAwaiter
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.AwaitOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);

            WaitingCoroutineRegisterNotifier.Register<TAwaiter>(ref awaiter, _coroutine);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICoroutineAwaiter
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.AwaitUnsafeOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);

            WaitingCoroutineRegisterNotifier.Register<TAwaiter>(ref awaiter, _coroutine);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.Start");
            _runner.Post(stateMachine.MoveNext);
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine _)
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.SetStateMachine");
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly struct AwaitableCoroutineMethodBuilder<T>
    {
        private sealed class Coroutine : AwaitableCoroutine<T>
        {
            public Coroutine(ICoroutineRunner runner) : base(runner) { }

            public void CallComplete(T result) => Complete(result);

            protected override void OnMoveNext() { }
        }

        private readonly ICoroutineRunner _runner;
        private readonly Coroutine _coroutine;

        private AwaitableCoroutineMethodBuilder(ICoroutineRunner runner)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}> constructor");
            _runner = runner;
            _coroutine = new Coroutine(runner);
        }

        // 1. Static Create method.
        public static AwaitableCoroutineMethodBuilder<T> Create()
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.Create");
            return new AwaitableCoroutineMethodBuilder<T>(ICoroutineRunner.GetContext());
        }

        // 2. TaskLike Task Property
        public AwaitableCoroutine<T> Task => _coroutine;

        // 3. SetException
        public void SetException(Exception exn)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.SetException");
            _coroutine.SetException(exn);
        }

        // 4. SetResult
        public void SetResult(T result)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.SetResult");
            _coroutine.CallComplete(result);
        }

        // 5. AwaitOnCompleted
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICoroutineAwaiter
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.AwaitOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
            WaitingCoroutineRegisterNotifier.Register<TAwaiter>(ref awaiter, _coroutine);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICoroutineAwaiter
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.AwaitUnsafeOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);

            WaitingCoroutineRegisterNotifier.Register<TAwaiter>(ref awaiter, _coroutine);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.Start");
            _runner.Post(stateMachine.MoveNext);
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine _)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.SetStateMachine");
        }
    }
}
