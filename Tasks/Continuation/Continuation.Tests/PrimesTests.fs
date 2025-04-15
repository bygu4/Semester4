module PrimesTests

open NUnit.Framework
open FsUnit

open Primes

[<Test>]
let testIsPrime () =
    isPrime 0 |> should be False
    isPrime 1 |> should be False
    isPrime 2 |> should be True
    isPrime 3 |> should be True
    isPrime 73 |> should be True
    isPrime 32132 |> should be False
    isPrime -9091 |> should be False
    isPrime 115249 |> should be True

[<Test>]
let testPrimes () =
    let primes = primes ()
    primes |> Seq.take 10 |> should equal (seq { 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 })
    primes |> Seq.item 41 |> should equal 181
    primes |> Seq.item 978 |> should equal 7717
    let primes = Seq.take 100000 primes
    primes |> should be ascending
    primes |> should be unique
    primes |> Seq.filter isPrime |> should equal primes
