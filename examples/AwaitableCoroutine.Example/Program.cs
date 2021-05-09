using System;

namespace AwaitableCoroutine.Example
{
    public class Program
    {
        private static int s_count;

        private static async AwaitableCoroutine CreateCoroutine()
        {
            for (var i = 0; i < 10; i++)
            {
                s_count++;
                await AwaitableCoroutine.Yield();
            }
        }

        public static void Main(string[] _)
        {
            var runner = new CoroutineRunner();

            var coroutine = runner.Context(CreateCoroutine);

            Console.WriteLine("Started!");

            while (!coroutine.IsCompleted)
            {
                Console.WriteLine($"{s_count}");
                runner.Update();
            }

            Console.WriteLine("Finished!");
        }
    }
}
