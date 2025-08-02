module StringCalculation.Tests

open NUnit.Framework
open FsUnit

open ComputationExpressions

[<Test>]
let testCalculation_SingleInteger_ReturnValue () =
    let calculation = StringCalculationBuilder ()
    calculation {
        let! a = "-4212"
        return a
    } |> should equal (Some -4212)

[<Test>]
let testCalculation_RegularCalculations_ReturnResult () =
    let calculation = StringCalculationBuilder ()
    calculation {
        let! x = "341"
        let! y = "-10"
        let! z = "0"
        let! t = "12"
        let res = (x + z) * y - t
        return res
    } |> should equal (Some -3422)

[<Test>]
let testCalculation_NotAnInteger_ReturnNone () =
    let calculation = StringCalculationBuilder ()
    calculation {
        let! a = "123"
        let! b = "ololo"
        let! c = "-1"
        let d = a / b + c
        return d
    } |> should equal None
