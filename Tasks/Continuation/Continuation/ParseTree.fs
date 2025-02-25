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
    match expr with
    | Const x -> x
    | Operator (op, left, right) ->
        operation op (evaluate left) (evaluate right)
