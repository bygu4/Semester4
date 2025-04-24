namespace ComputationExpressions

open System

type StringCalculationBuilder () =
    let tryParseInt (input: string) =
        match Int32.TryParse input with
        | true, number -> Some number
        | false, _ -> None

    member _.Bind (value: string, cont: int -> option<int>) =
        match tryParseInt value with
        | Some number -> cont number
        | None -> None

    member _.Return (value: int) = Some value
