using System;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine.Internal
{
    public sealed class AwaitableCoroutineMethodBuilder
    {
        private sealed class Coroutine : AwaitableCoroutine
        {
            public Coroutine() { }

            public void CallComplete() => Complete();

            public override void MoveNext() { }
        }

        private readonly Coroutine _coroutine;

        public AwaitableCoroutineMethodBuilder()
        {
            Logger.Log("AwaitableCoroutineMethodBuilder constructor");
            _coroutine  = new Coroutine().SetupRunner();
        }

        // 1. Static Create method.
        public static AwaitableCoroutineMethodBuilder Create() => new AwaitableCoroutineMethodBuilder();

        // 2. TaskLike Task Property
        public AwaitableCoroutine Task => _coroutine;

        // 3. SetException
        public void SetException(Exception _)
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.SetException");
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
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.AwaitOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.AwaitUnsafeOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.Start");

            var runner = ICoroutineRunner.Peek();

            if (runner is null)
            {
                throw new InvalidOperationException("Out of ICoroutineRunner context");
            }

            runner.Post(stateMachine.MoveNext);
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine _)
        {
            Logger.Log("AwaitableCoroutineMethodBuilder.SetStateMachine");
        }
    }

    public sealed class AwaitableCoroutineMethodBuilder<T>
    {
        private sealed class Coroutine : AwaitableCoroutine<T>
        {
            public Coroutine()
            {

            }

            public void CallComplete(T result) => Complete(result);

            public override void MoveNext() { }
        }

        private readonly Coroutine _coroutine;

        public AwaitableCoroutineMethodBuilder()
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}> constructor");
            _coroutine = new Coroutine().SetupRunner();
        }

        // 1. Static Create method.
        public static AwaitableCoroutineMethodBuilder<T> Create() => new AwaitableCoroutineMethodBuilder<T>();

        // 2. TaskLike Task Property
        public AwaitableCoroutine<T> Task => _coroutine;

        // 3. SetException
        public void SetException(Exception _)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.SetException");
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
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.AwaitOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.AwaitUnsafeOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.Start");

            var runner = ICoroutineRunner.Peek();

            if (runner is null)
            {
                throw new InvalidOperationException("Out of ICoroutineRunner context");
            }

            runner.Post(stateMachine.MoveNext);
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine _)
        {
            Logger.Log($"AwaitableCoroutineMethodBuilder<{typeof(T).Name}>.SetStateMachine");
        }
    }
}
