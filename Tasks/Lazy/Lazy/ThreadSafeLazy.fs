namespace Lazy

open System

type ThreadSafeLazy<'a when 'a: equality>(supplier: unit -> 'a) =
    [<VolatileFieldAttribute>]
    let mutable supplier = Some supplier
    let mutable result: 'a option = None
    let mutable thrownException: Exception option = None
    let lockObject = new Object ()

    let tryEvaluate () =
        lock lockObject (fun () ->
            if supplier.IsNone then ()
            try
                result <- Some (supplier.Value ())
            with
                | e -> thrownException <- Some e
            supplier <- None
        )

    let getResult () =
        tryEvaluate ()
        match result, thrownException with
        | Some value, _ -> value
        | _, Some e -> raise e
        | _ -> raise (new ArgumentException "unexpected match pattern")

    interface ILazy<'a> with
        member _.Get (): 'a = getResult ()

    member _.Get (): 'a = getResult ()
