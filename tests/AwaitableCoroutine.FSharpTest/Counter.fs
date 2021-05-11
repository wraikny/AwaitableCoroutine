namespace FSharpTest

type Counter() =
  let mutable count = 0

  member __.Count with get() = count

  member __.Inc() = count <- count + 1
