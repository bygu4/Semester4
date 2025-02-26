module Introduction.Tests

open NUnit.Framework

[<Test>]
let TestFactorial () =
    Assert.That(factorial -1 = Error "n should be non-negative")
    Assert.That(factorial 0 = Ok 1)
    Assert.That(factorial 1 =  Ok 1)
    Assert.That(factorial 4 = Ok 24)
    Assert.That(factorial 12 = Ok 479001600)

[<Test>]
let TestFibonacci () =
    Assert.That(fibonacci -43 = Error "n should be a natural number")
    Assert.That(fibonacci 0 = Error "n should be a natural number")
    Assert.That(fibonacci 1 = Ok 1)
    Assert.That(fibonacci 2 = Ok 1)
    Assert.That(fibonacci 6 = Ok 8)
    Assert.That(fibonacci 32 = Ok 2178309)

[<Test>]
let TestReverse () =
    Assert.That(reverse [] = [])
    Assert.That(reverse ["a"] = ["a"])
    Assert.That(reverse [1; 2; 3] = [3; 2; 1])
    Assert.That(reverse [for x in 1 .. 100 -> x] = [for x in 100 .. -1 .. 1 -> x])

[<Test>]
let TestFindElement () =
    Assert.That(findElement -1 [1; 2; 3] = Error "element was not present in the list")
    Assert.That(findElement 0 [] = Error "element was not present in the list")
    Assert.That(findElement 100 [12; 100; -100; 0; -1] = Ok 1)
    Assert.That(findElement "d" ["a"; "b"; "c"; "d"] = Ok 3)
    Assert.That(findElement 42.0 [42.0] = Ok 0)

[<Test>]
let TestPowersOfTwo () =
    Assert.That(fun () -> powersOfTwo 100 -1 = Error "m should be non-negative")
    Assert.That(powersOfTwo 1 5 = Ok [2.0; 4.0; 8.0; 16.0; 32.0; 64.0])
    Assert.That(powersOfTwo 0 2 = Ok [1.0; 2.0; 4.0])
    Assert.That(powersOfTwo 10 1 = Ok [1024.0; 2048.0])
    Assert.That(powersOfTwo 8 0 = Ok [256.0])
    Assert.That(powersOfTwo 0 0 = Ok [1.0])
    Assert.That(powersOfTwo -1 0 = Ok [0.5])
    Assert.That(powersOfTwo -2 5 = Ok [0.25; 0.5; 1.0; 2.0; 4.0; 8.0])
