module InfiniteSequence

let signAlternatingOnes =
    Seq.initInfinite (( + ) 1)
    |> Seq.map (fun x ->
        match x with
        | x when x % 2 = 0 -> -1
        | _ -> 1
    )

let signAlternatingSequence =
    Seq.initInfinite (( + ) 1)
    |> Seq.zip signAlternatingOnes
    |> Seq.map (fun (x, y) -> x * y)
