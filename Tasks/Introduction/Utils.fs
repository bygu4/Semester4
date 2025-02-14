namespace Introduction

module Utils =
    let product =
        Seq.reduce ( * )

    let firstNProduct n =
        Seq.take n >> product

    let factorial n = 
        if n <> 0 then product { 1 .. n } else 1

    let fibonacci n =
        List.head ({ 1 .. n - 2 } |> Seq.fold (fun acc _ -> firstNProduct 2 acc :: acc) [1; 1])

    let reverse list =
        List.fold (fun acc x -> x :: acc) [] list

    let findElement element =
        List.find (fun x -> x = element)

    let powersOfTwo n m =
        { 1 .. m } |> Seq.fold (fun acc _ -> List.head acc / 2 :: acc) [pown 2 (n + m)]
