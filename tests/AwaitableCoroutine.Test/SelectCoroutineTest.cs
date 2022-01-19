using Xunit;
using Xunit.Abstractions;

namespace AwaitableCoroutine.Test
{
    public class SelectCoroutineTest : TestTemplate
    {
        public SelectCoroutineTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Fact]
        public void RunSelect()
        {
            var runner = new CoroutineRunner();

            var (c1, c2, c3) = runner.Context(() =>
            {
                var c1 = AwaitableCoroutine.DelayCount(0);
                var c2 = c1.SelectTo(2);
                var c3 = c2.Select(x => x * x);
                return (c1, c2, c3);
            });

            Assert.False(c1.IsCompletedSuccessfully);
            Assert.False(c2.IsCompletedSuccessfully);
            Assert.False(c3.IsCompletedSuccessfully);

            runner.Update();
            Assert.True(c1.IsCompletedSuccessfully);
            Assert.True(c2.IsCompletedSuccessfully);
            Assert.True(c3.IsCompletedSuccessfully);

            Assert.Equal(2, c2.Result);
            Assert.Equal(4, c3.Result);
        }
    }
}
