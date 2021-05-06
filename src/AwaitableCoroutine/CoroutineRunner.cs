using System;
using System.Collections.Generic;

namespace AwaitableCoroutine
{
    public sealed class CoroutineRunner : ICoroutineRunner
    {
        private readonly Queue<Action> _continuations;

        private List<AwaitableCoroutineBase> _coroutines;
        private List<AwaitableCoroutineBase> _coroutinesTmp;

        public int Count { get; private set; }

        public CoroutineRunner()
        {
            _continuations = new Queue<Action>();
            _coroutines = new List<AwaitableCoroutineBase>();
            _coroutinesTmp = new List<AwaitableCoroutineBase>();
        }

        public void Register(AwaitableCoroutineBase coroutine)
        {
            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            if (coroutine.IsRegistered)
            {
                throw new InvalidOperationException("Coroutine is already registered.");
            }

            coroutine.SetRegistered();
            _coroutinesTmp.Add(coroutine);
            Count++;
        }

        public void Run(AwaitableCoroutineBase coroutine) => _coroutinesTmp.Add(coroutine);

        public void Update()
        {
            var contCount = _continuations.Count;
            for (var i = 0; i < contCount; i++)
            {
                _continuations.Dequeue().Invoke();
            }

            _coroutines.AddRange(_coroutinesTmp);

            _coroutinesTmp.Clear();

            foreach (var c in _coroutines)
            {
                c.MoveNext();

                if (!c.IsCompleted)
                {
                    _coroutinesTmp.Add(c);
                }
            }

            Count -= (_coroutines.Count - _coroutinesTmp.Count);

            // Swap
            (_coroutines, _coroutinesTmp) = (_coroutinesTmp, _coroutines);

            _coroutinesTmp.Clear();
        }

        void ICoroutineRunner.Post(Action continuation)
        {
            if (continuation is Action)
            {
                Console.WriteLine("Continuation posted");
                _continuations.Enqueue(() => {
                    Console.WriteLine("Start Coutinuatin");
                    continuation();
                    Console.WriteLine("End Coutinuatin");
                });
            }
        }
    }
}
