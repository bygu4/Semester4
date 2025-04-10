module InfiniteSequence

/// A sign alternating sequence of ones.
/// (1, -1, 1, -1, ...)
let signAlternatingOnes =
    Seq.initInfinite (( + ) 1)
    |> Seq.map (fun x ->
        match x with
        | x when x % 2 = 0 -> -1
        | _ -> 1
    )

/// A sign alternating sequence with ascending module.
/// (1, -2, 3, -4, ...)
let signAlternatingSequence =
    Seq.initInfinite (( + ) 1)
    |> Seq.zip signAlternatingOnes
    |> Seq.map (fun (x, y) -> x * y)
