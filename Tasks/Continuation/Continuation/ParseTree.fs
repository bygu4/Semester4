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

let rec evaluate expr =
    let rec evaluateInternal expr cont = 
        match expr with
        | Const x -> cont x
        | Operator (op, left, right) ->
            evaluateInternal left (fun leftValue ->
                evaluateInternal right (fun rightValue ->
                    operation op leftValue rightValue |> cont
                )
            )

    evaluateInternal expr id
