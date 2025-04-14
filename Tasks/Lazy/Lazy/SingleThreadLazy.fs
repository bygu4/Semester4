namespace Lazy

open System

type SingleThreadLazy<'a when 'a: equality>(supplier: unit -> 'a) =
    let mutable supplier = Some supplier
    let mutable result: 'a option = None
    let mutable thrownException: Exception option = None

    let tryEvaluate () =
        if supplier.IsSome then
            try
                result <- Some (supplier.Value ())
            with
                | e -> thrownException <- Some e
            supplier <- None

    let getResult () =
        tryEvaluate ()
        match result, thrownException with
        | Some value, _ -> value
        | _, Some e -> raise e
        | _ -> raise (new ArgumentException "unexpected match pattern")

    interface ILazy<'a> with
        member _.Get (): 'a = getResult ()

    member _.Get (): 'a = getResult ()
