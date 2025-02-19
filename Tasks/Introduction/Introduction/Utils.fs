namespace Introduction

open System

module Utils =
    let product =
        Seq.reduce ( * )

    let repeat n action target =
        { 1 .. n } |> Seq.fold (fun acc _ -> action acc) target

    let factorial n = 
        if n <> 0 then product { 1 .. n } else 1

    let fibonacci n =
        if n > 0 then List.head (repeat (n - 2) (fun acc -> List.sum acc :: [List.head acc]) [1; 1])
        else raise (ArgumentException "n should be a natural number")

    let reverse list =
        List.fold (fun acc x -> x :: acc) [] list

    let findElement element =
        List.findIndex (fun x -> x = element)

    let powersOfTwo n m =
        if m >= 0 then repeat m (fun acc -> List.head acc / 2 :: acc) [pown 2 (n + m)]
        else raise (ArgumentException "m should be non-negative")
