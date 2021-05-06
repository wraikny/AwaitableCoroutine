using System;
using System.Runtime.CompilerServices;

namespace AwaitableCoroutine
{
    public class AwaitableCoroutineMethodBuilder
    {
        private sealed class Coroutine : AwaitableCoroutine
        {
            public Coroutine(ICoroutineRunner runner)
                :base(runner)
            {

            }

            public void CallComplete() => Complete();

            public override void MoveNext() { }
        }

        private readonly Coroutine _coroutine;

        public AwaitableCoroutineMethodBuilder(ICoroutineRunner runner)
        {
            _coroutine  = new Coroutine(runner);
        }

        // 1. Static Create method.
        public static AwaitableCoroutineMethodBuilder Create() => new AwaitableCoroutineMethodBuilder(ICoroutineRunner.Instance);

        // 2. TaskLike Task Property
        public AwaitableCoroutine Task => _coroutine;

        // 3. SetException
        public void SetException(Exception _) { }

        // 4. SetResult
        public void SetResult()
        {
            Console.WriteLine("SetResult");
            _coroutine.CallComplete();
        }

        // 5. AwaitOnCompleted
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("AwaitOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("AwaitUnsafeOnCompleted");
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("Start");
            if (ICoroutineRunner.Instance is null)
            {
                throw new InvalidOperationException($"{nameof(ICoroutineRunner.Instance)} is null");
            }
            ICoroutineRunner.Instance.Post(stateMachine.MoveNext);
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine _)
        {
            Console.WriteLine("SetStateMachine");
        }
    }
}
