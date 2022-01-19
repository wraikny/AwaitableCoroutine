# AwaitableCoroutine Document

**Table of contents**
- [AwaitableCoroutine Document](#awaitablecoroutine-document)
  - [What is AwaitableCoroutine?](#what-is-awaitablecoroutine)
  - [What it does.](#what-it-does)
  - [How to use](#how-to-use)
  - [Notes.](#notes)
  - [AwaitableCoroutine, AwaitableCoroutine<T> classes](#awaitablecoroutine-awaitablecoroutinet-classes)
  - [ICoroutineRunner interface](#icoroutinerunner-interface)
  - [AwaitableCoroutine.FSharp package](#awaitablecoroutinefsharp-package)


## What is AwaitableCoroutine?

This package provides `AwaitableCoroutine`, a coroutine that can use async/await syntax in C#.

## What it does.

* Coroutines can be written in C# using async/await syntax.
* Coroutines are executed by explicitly calling the runner `Update`, so you can easily control the update timing.
* Coroutines can wait for executed coroutines.
* Coroutines can be written inline using asynchronous lambda expressions.
* Coroutines can be canceled.
* When a coroutine is canceled, all coroutines waiting for that coroutine are also canceled together.
* A variety of useful methods are provided as standard.
* If you are concerned about the cost of state machine generation, you can create your own using inheritance.
* The behavior of some processes can be rewritten by using interfaces.
* Computation expressions based on TaskBuilder.fs are provided for F#. (AwaitableCoroutine.FSharp package)


## How to use

For example, you can define a coroutine as follows

```csharp
// AwaitableCoroutine can be created using the async method.
private static async AwaitableCoroutine CreateCoroutine()
{
    while (true)
    {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello {i}");
            
            // Use `Yield` to wait (suspend) with await only once.
            await AwaitableCoroutine.Yield();
        }

        Console.WriteLine("Start delay");

        // Create a coroutine that executes for the specified count, and wait with `await`.
        await AwaitableCoroutine.DelayCount(10);
    }
}
```

And call it as follows.

```csharp
public static void Main(string[] _)
{
    // Create an instance of Runner
    var runner = new CoroutineRunner();

    // Note: the `AwaitableCoroutine` must be created in a callback passed to the `Create` or `Context` extension methods
    var coroutine = runner.Create(CreateCoroutine);
    /*
      // When using the `Context` extension method
      var coroutine = runner.Context(CreateCoroutine);
    */

    // Main loop
    while(!coroutine.IsCompletedSuccessfully)
    {
         // Move registered coroutines to the next by calling the `Update` extension method of ICoroutineRunner
        runner.Update();
    }
}
```

Asynchronous lambda expressions can also be used to generate coroutines.

```csharp
public static void Main(string[] _)
{
    var runner = new CoroutineRunner();

    // Create an `AwaitableCoroutine` using an asynchronous lambda expression.
    var coroutine = runner.Create(async () => {
        for (var i = 0; i < 5; i++)
        {
            Console.WriteLine($"Hello with async lambda {i}");
            await AwaitableCoroutine.Yield();
        }
    });

    // Note: When using asynchronous lambda expressions in `Context` methods, Explicit declaration of generic parameters is required.
    /*
      var coroutine = runner.Context<AwaitableCoroutine>(async () => {
        await AwaitableCoroutine.Yield();
      });
    */

    while(!coroutine.IsCompletedSuccessfully)
    {
        runner.Update();
    }
}
```

## Notes.

Note that `AwaitableCoroutine` and `AwaitableCoroutine<T>` are generated in the callback function of the `Create` extension method or the `Context` extension method.
It is needed to give information about which `ICoroutineRunner` the coroutine should be registered with.
Basically, use the `Create` extension method.

If you want to return a type other than `AwaitableCoroutien` or `AwaitableCoroutien<T>`, such as returning a tuple of `AwaitableCoroutine`, use the `Context` extension method as follows.

```csharp
var (c1, c2) = runner.Context(() => (AwaitableCoroutine.DelayCount(1), AwaitableCoroutine.DelayCount(1)));
````

Be careful when creating an `AwaitableCoroutine` using asynchronous lambda expressions, because it will be inferred as a `Task` if you don't specify the generic parameters.

If you want to give an argument to the creation of a coroutine, you can do so as follows.

```csharp
Context(() => FooBarCoroutine(arg1, arg2)). var coroutine = runner;
```

## AwaitableCoroutine, AwaitableCoroutine<T> classes

Classes for awaitable coroutines.

See [AwaitableCoroutine.md](AwaitableCoroutine.md).

## ICoroutineRunner interface

This interface is the context for executing a coroutine.

See [ICoroutineRunner.md](ICoroutineRunner.md).

## AwaitableCoroutine.FSharp package

Extension package for F#.

See [AwaitableCoroutine.FSharp.md](AwaitableCoroutine.FSharp.md).

<!-- 
## AwaitableCoroutine.Altseed2 package

Extension package for the Altseed2 game engine.

See [AwaitableCoroutine.Altseed2.md](AwaitableCoroutine.Altseed2.md). -->
