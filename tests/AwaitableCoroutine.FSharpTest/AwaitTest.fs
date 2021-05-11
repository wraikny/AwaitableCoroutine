module FSharpTest.Tests

open System
open Xunit

open AwaitableCoroutine
open AwaitableCoroutine.FSharp

[<Fact>]
let ``Yield Test`` () =
  let runner = CoroutineRunner()

  let counter = new Counter()

  let ac = runner.Context(fun () ->
      awaitableCoroutine {
        counter.Inc()
        do! AwaitableCoroutine.Yield()
        counter.Inc()
      }
  )

  Assert.False(ac.IsCompleted)
  Assert.Equal(0, counter.Count)

  runner.Update()
  Assert.Equal(1, counter.Count)

  runner.Update()
  Assert.True(ac.IsCompleted)
  Assert.Equal(2, counter.Count)

[<Fact>]
let ``ForLoop Test`` () =
  let runner = CoroutineRunner()

  let counter = new Counter()

  let ac = runner.Context(fun () ->
      awaitableCoroutine {
        counter.Inc()
        for i=1 to 3 do
          do! AwaitableCoroutine.Yield()
        counter.Inc()
      }
  )

  Assert.False(ac.IsCompleted)
  Assert.Equal(0, counter.Count)

  runner.Update()
  Assert.Equal(1, counter.Count)

  for i in 1..3 do runner.Update()

  Assert.True(ac.IsCompleted)
  Assert.Equal(2, counter.Count)

