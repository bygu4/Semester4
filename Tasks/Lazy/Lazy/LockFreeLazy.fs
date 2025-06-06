namespace Lazy

open System
open System.Threading

/// Class representing thread safe lazy evaluation without locks.
/// Creates an instance with the given `supplier` to evaluate.
type LockFreeLazy<'a when 'a: equality>(supplier: unit -> 'a) =
    [<VolatileField>]
    let mutable supplier = Some supplier
    let initialResult: 'a option * exn option = None, None
    let mutable result = initialResult

    /// Run supplier and set the result if it was not already set, then clear the supplier.
    let tryEvaluate () =
        let currentSupplier = supplier
        match currentSupplier with
        | Some func ->
            try
                let localResult = Some (func ())
                Interlocked.CompareExchange (&result, (localResult, None), initialResult)
                |> ignore
            with
                | e ->
                    Interlocked.CompareExchange (&result, (None, Some e), initialResult)
                    |> ignore
            supplier <- None
        | None -> ()

    /// Get the result evaluated lazily.
    let getResult () =
        tryEvaluate ()
        match result with
        | Some value, _ -> value
        | _, Some e -> raise e
        | _ -> raise (new ArgumentException "unexpected match pattern")

    interface ILazy<'a> with
        member _.Get (): 'a = getResult ()

    /// Get the result evaluated lazily.
    member _.Get (): 'a = getResult ()
