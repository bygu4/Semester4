module RoundingWorkflow.Tests

open NUnit.Framework
open FsUnit
open System

open ComputationExpressions

[<Test>]
let testRounding_NegativePrecision_ThrowException () =
    (fun () -> new RoundingWorkflowBuilder -1 |> ignore)
    |> should throw typeof<ArgumentOutOfRangeException>

[<Test>]
let testRounding_PrecisionZero_GetAnInteger () =
    let rounding = new RoundingWorkflowBuilder 0
    rounding {
        let! a = -43.908
        return a
    } |> should equal -44.0

[<Test>]
let testRounding_RegularCalculations_GetRoundedResult () =
    let rounding = new RoundingWorkflowBuilder 2
    rounding {
        let! pi = 3.141592
        let! r = 55.2301
        let! s = pi * r * r
        return s
    } |> should equal 9578.11

[<Test>]
let testRounding_CalculationsWithDivision_GetRoundedResult () =
    let rounding = new RoundingWorkflowBuilder 3
    rounding {
        let! a = 12.5 / 4.0
        let! b = 0.01 / 10.0
        let c = a + b
        return c
    } |> should equal 3.126
