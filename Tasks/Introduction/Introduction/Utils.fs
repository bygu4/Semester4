namespace Introduction

open System
open System.Collections.Generic

module Utils =
    let rec fold folder state source =
        if Seq.isEmpty source then state
        else fold folder (folder state (Seq.head source)) (Seq.tail source)

    let reduce reduction source =
        fold reduction (Seq.head source) (Seq.tail source)

    let sum = 
        reduce ( + )

    let product =
        reduce ( * )

    let repeat n action target =
        { 1 .. n } |> fold (fun acc _ -> action acc) target

    let factorial n = 
        if n <> 0 then product { 1 .. n } else 1

    let fibonacci n =
        if n > 0 then List.head (repeat (n - 2) (fun acc -> sum acc :: [List.head acc]) [1; 1])
        else raise (ArgumentException "n should be a natural number")

    let reverse target =
        fold (fun acc x -> x :: acc) [] target

    let findElement element list =
        let rec findInternal element list index =
            match list with
            | [] -> raise (KeyNotFoundException "element was not present in the list")
            | curr :: tail ->
                if curr = element then index
                else findInternal element tail (index + 1)
        findInternal element list 0

    let powersOfTwo n m =
        if m >= 0 then repeat m (fun acc -> List.head acc / 2 :: acc) [pown 2 (n + m)]
        else raise (ArgumentException "m should be non-negative")
