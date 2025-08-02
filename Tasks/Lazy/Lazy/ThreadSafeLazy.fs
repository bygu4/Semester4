namespace Lazy

open System

/// Class representing thread safe lazy evaluation.
/// Creates an instance with the given `supplier` to evaluate.
type ThreadSafeLazy<'a when 'a: equality>(supplier: unit -> 'a) =
    [<VolatileField>]
    let mutable supplier = Some supplier
    let mutable result: 'a option = None
    let mutable thrownException: Exception option = None
    let lockObject = new Object ()

    /// Run supplier if it was not already run, set result and clear the supplier.
    let tryEvaluate () =
        lock lockObject (fun () ->
            match supplier with
            | Some func ->
                try
                    result <- Some (func ())
                with
                    | e -> thrownException <- Some e
                supplier <- None
            | None -> ()
        )

    /// Get the result evaluated lazily.
    let getResult () =
        tryEvaluate ()
        match result, thrownException with
        | Some value, _ -> value
        | _, Some e -> raise e
        | _ -> raise (new ArgumentException "unexpected match pattern")

    interface ILazy<'a> with
        member _.Get (): 'a = getResult ()

    /// Get the result evaluated lazily.
    member _.Get (): 'a = getResult ()
