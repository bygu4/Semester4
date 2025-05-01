module ParseTreeTests

open NUnit.Framework
open FsUnit
open System

open ParseTree

[<Test>]
let testEvaluate () =
    let ``1 + 1`` = Operator (Sum, Const 1, Const 1)
    let ``25 - 100`` = Operator (Difference, Const 25, Const 100)
    let ``8 * 5`` = Operator (Product, Const 8, Const 5)
    let ``8 * 5 + 25 - 100`` = Operator (Sum, ``8 * 5``, ``25 - 100``)
    let ``14 / (1 + 1)`` = Operator (Ratio, Const 14, ``1 + 1``)
    let ``14 / (1 + 1) - 9`` = Operator (Difference, ``14 / (1 + 1)``, Const 9)
    let ``3 * (14 / (1 + 1) - 9)`` = Operator (Product, Const 3, ``14 / (1 + 1) - 9``)
    let ``321 * 0`` = Operator (Product, Const 321, Const 0)
    let ``0 / 0`` = Operator (Ratio, Const 0, Const 0)
    let ``(1 + 1) / (321 * 0)`` = Operator (Ratio, ``1 + 1``, ``321 * 0``)

    evaluate (Const 0) |> should equal 0
    evaluate ``1 + 1`` |> should equal 2
    evaluate ``25 - 100`` |> should equal -75
    evaluate ``8 * 5`` |> should equal 40
    evaluate ``8 * 5 + 25 - 100`` |> should equal -35
    evaluate ``14 / (1 + 1)`` |> should equal 7
    evaluate ``14 / (1 + 1) - 9`` |> should equal -2
    evaluate ``3 * (14 / (1 + 1) - 9)`` |> should equal -6
    evaluate ``321 * 0`` |> should equal 0
    (fun () -> evaluate ``0 / 0`` |> ignore) |> should throw typeof<DivideByZeroException>
    (fun () -> evaluate ``(1 + 1) / (321 * 0)`` |> ignore) |> should throw typeof<DivideByZeroException>

[<Test>]
let testEvaluateWithLargeExpression () =
    let treeDepth = 1000000
    { 1 .. treeDepth }
    |> Seq.fold (fun node _ -> Operator (Sum, node, Const 1)) (Const 1)
    |> evaluate
    |> should equal (treeDepth + 1)
