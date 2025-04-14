module Lazy.Tests

open NUnit.Framework
open FsUnit

open System
open System.Threading

let simpleSuppliers: (unit -> obj) list = [
    fun () -> 80808;
    fun () -> "qwertyuiop".Substring (0, 6);
    fun () -> Math.Round (782.192, 1);
]

let suppliersWithDelay =
    simpleSuppliers |> List.map (fun f ->
        Thread.Sleep 100
        f ()
    )

let suppliersWithSideEffects =
    simpleSuppliers |> List.map (fun f ->
        count <- count + 1
        f ()
    )

let suppliersThrowingException: (unit -> obj) list = [
    fun () ->
        raise (new InvalidOperationException ())
        100;
    fun () ->
        if count > 1 then raise (new ArgumentException ())
        "abc";
]

let singleThreadTestCase (supplier: unit -> obj) =
