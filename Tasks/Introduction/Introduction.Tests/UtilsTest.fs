module Introduction.Tests

open NUnit.Framework
open System
open System.Collections.Generic

open Utils

[<Test>]
let TestFactorial () =
    Assert.Throws<ArgumentException>(fun () -> factorial -1 |> ignore) |> ignore
    Assert.That(factorial 0 = 1)
    Assert.That(factorial 1 = 1)
    Assert.That(factorial 4 = 24)
    Assert.That(factorial 12 = 479001600)

[<Test>]
let TestFibonacci () =
    Assert.Throws<ArgumentException>(fun () -> fibonacci -43 |> ignore) |> ignore
    Assert.Throws<ArgumentException>(fun () -> fibonacci 0 |> ignore) |> ignore
    Assert.That(fibonacci 1 = 1)
    Assert.That(fibonacci 2 = 1)
    Assert.That(fibonacci 6 = 8)
    Assert.That(fibonacci 32 = 2178309)

[<Test>]
let TestReverse () =
    Assert.That(reverse [] = [])
    Assert.That(reverse ["a"] = ["a"])
    Assert.That(reverse [1; 2; 3] = [3; 2; 1])
    Assert.That(reverse [for x in 1 .. 100 -> x] = [for x in 100 .. -1 .. 1 -> x])

[<Test>]
let TestFindElement () =
    Assert.Throws<KeyNotFoundException>(fun () -> findElement -1 [1; 2; 3] |> ignore) |> ignore
    Assert.Throws<KeyNotFoundException>(fun () -> findElement 0 [] |> ignore) |> ignore
    Assert.That(findElement 100 [12; 100; -100; 0; -1] = 1)
    Assert.That(findElement "d" ["a"; "b"; "c"; "d"] = 3)
    Assert.That(findElement 42.0 [42.0] = 0)

[<Test>]
let TestPowersOfTwo () =
    Assert.Throws<ArgumentException>(fun () -> powersOfTwo 100 -1 |> ignore) |> ignore
    Assert.That(powersOfTwo 1 5 = [2; 4; 8; 16; 32; 64])
    Assert.That(powersOfTwo 0 2 = [1; 2; 4])
    Assert.That(powersOfTwo 10 1 = [1024; 2048])
    Assert.That(powersOfTwo 8 0 = [256])
    Assert.That(powersOfTwo 0 0 = [1])
    Assert.That(powersOfTwo -1 0 = [0])
    Assert.That(powersOfTwo -2 5 = [0; 0; 1; 2; 4; 8])
