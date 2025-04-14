namespace Lazy

open System
open System.Threading

type LockFreeLazy<'a when 'a: equality>(supplier: unit -> 'a) =
    [<VolatileField>]
    let mutable supplier = Some supplier
    let mutable result: 'a option * Exception option = None, None

    let tryEvaluate () =
        let currentSupplier = supplier
        if currentSupplier.IsSome then
            try
                let localResult = Some (currentSupplier.Value ())
                Interlocked.CompareExchange (&result, (localResult, None), (None, None))
                |> ignore
            with
                | e ->
                    Interlocked.CompareExchange (&result, (None, Some e), (None, None))
                    |> ignore
            supplier <- None

    let getResult () =
        tryEvaluate ()
        match result with
        | Some value, _ -> value
        | _, Some e -> raise e
        | _ -> raise (new ArgumentException "unexpected match pattern")

    interface ILazy<'a> with
        member _.Get (): 'a = getResult ()

    member _.Get (): 'a = getResult ()
