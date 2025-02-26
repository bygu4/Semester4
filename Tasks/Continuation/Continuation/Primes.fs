module Primes

let isPrime n =
    if n >= 2 then
        { 2 .. int (sqrt (float n)) }
        |> Seq.exists (fun x -> n % x = 0)
        |> not
    else false

let primes () =
    ( + ) 2
    |> Seq.initInfinite
    |> Seq.filter isPrime
