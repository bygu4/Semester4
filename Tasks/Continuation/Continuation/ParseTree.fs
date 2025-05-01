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

[<TailCall>]
let rec evaluateCPS expr cont = 
    match expr with
    | Const x -> cont x
    | Operator (op, left, right) ->
        evaluateCPS left (fun leftValue ->
            evaluateCPS right (fun rightValue ->
                operation op leftValue rightValue |> cont
            )
        )

let evaluate expr = evaluateCPS expr id
