module Lazy.Tests

open NUnit.Framework
open FsUnit

open System
open System.Threading
open System.Threading.Tasks

let numberOfGets = 500
let numberOfThreads = 500

let mutable evaluationCount = 0

let testSupplier supplier = fun () ->
    Interlocked.Increment &evaluationCount |> ignore
    supplier ()

let suppliersWithResult: (unit -> obj) list = [
    fun () -> 80808;
    fun () -> "qwertyuiop".Substring (0, 6);
    fun () -> Math.Round (782.192, 1);
]

let suppliersWithException: (unit -> obj) list = [
    fun () ->
        raise (new InvalidOperationException ())
        100;
    fun () ->
        raise (new ArgumentException ())
        "abc";
]

let testSuppliersWithResult = suppliersWithResult |> List.map testSupplier
let testSuppliersWithException = suppliersWithException |> List.map testSupplier

let results: obj list = [80808; "qwerty"; 782.2]
let exceptions = [typeof<InvalidOperationException>; typeof<ArgumentException>]

let testCasesWithResult = Seq.zip testSuppliersWithResult results |> Seq.map TestCaseData
let testCasesWithException = Seq.zip testSuppliersWithException exceptions |> Seq.map TestCaseData

let testForSingleThread_WithResult (result: 'a) (testLazy: ILazy<'a>) =
    for _ in { 1 .. numberOfGets } do
        testLazy.Get () |> should equal result
    evaluationCount |> should equal 1

let testForSingleThread_WithException (exc: Type) (testLazy: ILazy<'a>) =
    for _ in { 1 .. numberOfGets } do
        testLazy.Get |> should throw exc
    evaluationCount |> should equal 1

[<SetUp>]
let resetEvaluationCount () =
    evaluationCount <- 0

[<TestCaseSourceAttribute(nameof(testCasesWithResult))>]
let testSingleThreadLazy_WithResult (supplier: unit -> obj, result: obj) =
    supplier
    |> SingleThreadLazy
    |> testForSingleThread_WithResult result

[<TestCaseSourceAttribute(nameof(testCasesWithException))>]
let testSingleThreadLazy_WithException (supplier: unit -> obj, exc: Type) =
    supplier
    |> SingleThreadLazy
    |> testForSingleThread_WithException exc

[<TestCaseSourceAttribute(nameof(testCasesWithResult))>]
let testThreadSafeLazy_WithResult (supplier: unit -> obj, result: obj) =
    let testLazy = supplier |> ThreadSafeLazy
    { 1 .. numberOfThreads }
    |> Seq.map (fun _ -> Task.Run (
        fun () -> testForSingleThread_WithResult result testLazy))
    |> Task.WaitAll

[<TestCaseSourceAttribute(nameof(testCasesWithException))>]
let testThreadSafeLazy_WithException (supplier: unit -> obj, exc: Type) =
    let testLazy = supplier |> ThreadSafeLazy
    { 1 .. numberOfThreads }
    |> Seq.map (fun _ -> Task.Run (
        fun () -> testForSingleThread_WithException exc testLazy))
    |> Task.WaitAll
