using System.Threading.Tasks;

namespace AwaitableCoroutine
{
    public partial class Coroutine
    {
        public static async Coroutine AwaitTask(Task task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    Coroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    Coroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }
        }

        public static async Coroutine<T> AwaitTask<T>(Task<T> task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    Coroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    Coroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }

            return task.Result;
        }

        public static async Coroutine AwaitTask(ValueTask task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    Coroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    Coroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }
        }

        public static async Coroutine<T> AwaitTask<T>(ValueTask<T> task)
        {
            while (!task.IsCompletedSuccessfully)
            {
                if (task.IsCanceled)
                {
                    Coroutine.ThrowCancel("task is canceled");
                }

                if (task.IsFaulted)
                {
                    Coroutine.ThrowCancel("task is faulted");
                }
                await Yield();
            }

            return task.Result;
        }
    }
}
