using System;
using System.Collections.Generic;

namespace AwaitableCoroutine
{
    public sealed class CoroutineRunner : ICoroutineRunner
    {
        private readonly Queue<Action> _continuations;

        private List<AwaitableCoroutineBase> _coroutines;
        private List<AwaitableCoroutineBase> _coroutinesTmp;

        public bool IsUpdating { get; private set; } = false;

        public int Count { get; private set; } = 0;

        public CoroutineRunner()
        {
            _continuations = new Queue<Action>();
            _coroutines = new List<AwaitableCoroutineBase>();
            _coroutinesTmp = new List<AwaitableCoroutineBase>();
        }

        void ICoroutineRunner.OnRegistering(AwaitableCoroutineBase coroutine)
        {
            Internal.Logger.Log($"CoroutineRunner.Registering {coroutine.GetType().Name}");
            _coroutinesTmp.Add(coroutine);
            Count++;
        }

        void ICoroutineRunner.OnUpdate()
        {
            if (IsUpdating)
            {
                throw new InvalidOperationException("Runnner is already updating");
            }

            IsUpdating = true;

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

                if (c.IsCompleted)
                {
                    Internal.Logger.Log($"CoroutineRunner.OnUpdate {c.GetType().Name} is completed");
                }
                else
                {
                    _coroutinesTmp.Add(c);
                }
            }

            Count -= (_coroutines.Count - _coroutinesTmp.Count);

            // Swap
            (_coroutines, _coroutinesTmp) = (_coroutinesTmp, _coroutines);

            _coroutinesTmp.Clear();

            IsUpdating = false;
        }

        void ICoroutineRunner.Post(Action continuation)
        {
            if (continuation is null) return;

            if (continuation is { })
            {
                _continuations.Enqueue(continuation);
            }
        }
    }
}
