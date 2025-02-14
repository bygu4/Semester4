namespace Introduction

open System

module Utils =
    let product =
        Seq.reduce ( * )

    let firstNSum n =
        Seq.take n >> Seq.sum

    let factorial n = 
        if n <> 0 then product { 1 .. n } else 1

    let fibonacci n =
        if n > 0 then List.head ({ 1 .. n - 2 } |> Seq.fold (fun acc _ -> firstNSum 2 acc :: acc) [1; 1])
        else raise (ArgumentException "n should be a natural number")

    let reverse list =
        List.fold (fun acc x -> x :: acc) [] list

    let findElement element =
        List.findIndex (fun x -> x = element)

    let powersOfTwo n m =
        if m >= 0 then { 1 .. m } |> Seq.fold (fun acc _ -> List.head acc / 2 :: acc) [pown 2 (n + m)]
        else raise (ArgumentException "m should be non-negative")
