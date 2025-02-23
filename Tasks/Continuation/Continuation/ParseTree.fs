namespace Continuation

module ParseTree =
    type Node =
    | Sum
    | Difference
    | Product
    | Ratio
    | Const of int

    let operation node =
        match node with
        | Sum -> ( + )
        | Difference -> ( - )
        | Product -> ( * )
        | Ratio -> ( / )
        | Const x -> fun _ _ -> x

    let rec evaluate tree =
        match tree with
        | Tree.Empty -> 0
        | Tree.Node (x, left, right)
            ->  let op = operation x
                op (evaluate left) (evaluate right)
