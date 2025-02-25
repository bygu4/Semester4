module Primes

let isPrime n =
    { 2 .. int (sqrt (float n)) }
    |> Seq.exists (fun x -> n % x = 0)
    |> not

let primes () =
    ( + ) 2
    |> Seq.initInfinite
    |> Seq.filter isPrime
