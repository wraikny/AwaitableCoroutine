using System;

using Altseed2;

using AwaitableCoroutine;
using AwaitableCoroutine.Altseed2;

using AwCo = AwaitableCoroutine.AwaitableCoroutine;
using A2Co = AwaitableCoroutine.Altseed2.Altseed2Coroutine;

static class Program
{
    [STAThread]
    static void Main()
    {
        Engine.Initialize("AwaitableCoroutine", 400, 300);

        var runner = new CoroutineNode();
        Engine.AddNode(runner);

        var node = new RectangleNode { RectangleSize = new Vector2F(10f, 10f)};
        Engine.AddNode(node);

        var coroutine = runner.Create(() => {
            var initPos = node.Position;
            return A2Co.DelaySecond(2f, a => {
                var ea = Easing.GetEasing(EasingType.InOutQuad, a);
                node.Position = initPos + new Vector2F(ea * 400f, 0f);
            });
        });

        while (Engine.DoEvents())
        {
            if (coroutine.IsCompleted) break;

            Engine.Update();
        }

        Engine.Terminate();
    }
}
