using System;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        private sealed class CoroutineObserver<T> : IObserver<T>
        {
            public T Result { get; private set; }
            public bool IsObserved { get; private set; } = false;
            public bool IsCompleted { get; private set; } = false;
            void IObserver<T>.OnCompleted() { IsCompleted = true; }
            void IObserver<T>.OnError(Exception error) { }
            void IObserver<T>.OnNext(T value)
            {
                Result = value;
                IsObserved = true;
            }
        }

        public static Coroutine<T> AwaitObservable<T>(IObservable<T> observable)
        {
            var observer = new CoroutineObserver<T>();

            var d = observable.Subscribe(observer);

            return Lambda(async () =>
            {
                while (!observer.IsObserved)
                {
                    if (observer.IsCompleted) Coroutine.ThrowCancel("waiting observable is completed.");
                    await Yield();
                }
                d.Dispose();
                return observer.Result;
            });
        }

        public static Coroutine AwaitObservableCompleted<T>(IObservable<T> observable)
        {
            var observer = new CoroutineObserver<T>();

            var d = observable.Subscribe(observer);

            return Lambda(async () =>
            {
                while (!observer.IsCompleted) await Yield();
                d.Dispose();
            });
        }
    }
}
