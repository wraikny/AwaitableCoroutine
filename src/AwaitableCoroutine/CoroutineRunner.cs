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

        void ICoroutineRunner.OnRegistering(AwaitableCoroutineBase coroutine)
        {
            _coroutinesTmp.Add(coroutine);
            Count++;
        }

        public void OnUpdate()
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
                _continuations.Enqueue(continuation);
            }
        }
    }
}
