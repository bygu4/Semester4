module ParseTree

type Expression =
    | Const of int
    | Operator of Operation * Expression * Expression
and Operation =
    | Sum
    | Difference
    | Product
    | Ratio

let operation op =
    match op with
    | Sum -> ( + )
    | Difference -> ( - )
    | Product -> ( * )
    | Ratio -> ( / )

let evaluate expr =

    let rec evaluate expr cont = 
        match expr with
        | Const x -> cont x
        | Operator (op, left, right) ->
            evaluate left (fun leftValue ->
                evaluate right (fun rightValue ->
                    operation op leftValue rightValue |> cont
                )
            )

    in evaluate expr id
