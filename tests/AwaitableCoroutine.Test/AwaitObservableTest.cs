using System;
using System.Collections.Generic;

using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class AwaitObservableTest : TestTemplate
    {
        public AwaitObservableTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        private class Unsubscribe<T> : IDisposable
        {
            private readonly List<IObserver<T>> observers;
            private readonly IObserver<T> observer;

            public Unsubscribe(List<IObserver<T>> observers, IObserver<T> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            void IDisposable.Dispose()
            {
                observers.Remove(observer);
            }
        }

        private class Observable<T> : IObservable<T>
        {
            private readonly List<IObserver<T>> observers;

            public Observable()
            {
                observers = new List<IObserver<T>>();
            }

            IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
            {
                observers.Add(observer);
                return new Unsubscribe<T>(observers, observer);
            }

            public void Notify(T value)
            {
                foreach (var o in observers)
                {
                    o.OnNext(value);
                }
            }

            public void Complete()
            {
                foreach (var o in observers)
                {
                    o.OnCompleted();
                }

                observers.Clear();
            }
        }
    }
}
