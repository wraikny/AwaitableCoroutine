using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace AwaitableCoroutine
{
    public sealed class CoroutineRunner : ICoroutineRunner
    {
        private readonly Queue<Action> _continuations;

        private List<CoroutineBase> _coroutines;
        private List<CoroutineBase> _coroutinesTmp;

        public bool IsUpdating { get; private set; } = false;

        public int Count { get; private set; } = 0;

        public CoroutineRunner()
        {
            _continuations = new Queue<Action>();
            _coroutines = new List<CoroutineBase>();
            _coroutinesTmp = new List<CoroutineBase>();
        }

        void ICoroutineRunner.OnRegistering(CoroutineBase coroutine)
        {
            Internal.Logger.Log($"CoroutineRunner.Registering {coroutine.GetType().Name}");
            _coroutinesTmp.Add(coroutine);
            Count++;
        }

        void ICoroutineRunner.OnUpdate()
        {
            if (IsUpdating)
            {
                ThrowHelper.InvalidOp("Runnner is already updating");
            }

            IsUpdating = true;

            List<Exception> exns = null;
            try
            {

                var contCount = _continuations.Count;

                for (var i = 0; i < contCount; i++)
                {
                    try
                    {
                        _continuations.Dequeue().Invoke();
                    }
                    catch (Exception e)
                    {
                        exns ??= new List<Exception>();
                        exns.Add(e);
                    }
                }

                _coroutines.AddRange(_coroutinesTmp);
                _coroutinesTmp.Clear();

                foreach (var c in _coroutines)
                {
                    if (!c.IsCompletedSuccessfully && !c.IsCanceled) c.MoveNext();

                    if (c.Exception is CanceledException)
                    {
                        // hack
                        Internal.Logger.Log($"CoroutineRunner.OnUpdate {c.GetType().Name} is canceled with {c.Exception.GetType()}");
                    }
                    else if (c.Exception is Exception e)
                    {
                        Internal.Logger.Log($"CoroutineRunner.OnUpdate {c.GetType().Name} has {e.GetType()}");

                        exns ??= new List<Exception>();
                        exns.Add(e);
                    }
                    else if (c.IsCanceled)
                    {
                        Internal.Logger.Log($"CoroutineRunner.OnUpdate {c.GetType().Name} is canceled");
                    }
                    else if (c.IsCompletedSuccessfully)
                    {
                        Internal.Logger.Log($"CoroutineRunner.OnUpdate {c.GetType().Name} is completed successfully");
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
            }
            finally
            {
                IsUpdating = false;
            }

            if (exns is { })
            {
                if (exns.Count == 1) ExceptionDispatchInfo.Capture(exns[0]).Throw();
                throw new AggregateException(exns).Flatten();
            }
        }

        void ICoroutineRunner.Post(Action continuation)
        {
            if (continuation is null) return;

            Internal.Logger.Log($"CoroutineRunner.Post");
            _continuations.Enqueue(continuation);
        }
    }
}
