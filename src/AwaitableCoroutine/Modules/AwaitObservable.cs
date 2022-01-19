using System;

namespace AwaitableCoroutine
{
    public partial class AwaitableCoroutine
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

        public static async AwaitableCoroutine<T> AwaitObservable<T>(IObservable<T> observable)
        {
            var observer = new CoroutineObserver<T>();

            using var d = observable.Subscribe(observer);

            while (!observer.IsObserved)
            {
                if (observer.IsCompleted) AwaitableCoroutine.ThrowCancel();
                await Yield();
            }

            return observer.Result;
        }

        public static async AwaitableCoroutine AwaitObservableCompleted<T>(IObservable<T> observable)
        {
            var observer = new CoroutineObserver<T>();

            using var d = observable.Subscribe(observer);

            while (!observer.IsCompleted) await Yield();
        }
    }
}
