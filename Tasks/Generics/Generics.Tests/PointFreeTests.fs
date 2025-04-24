module PointFree.Tests

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck.NUnit

[<Test>]
let testMultiplyBy () = 
    multiplyBy'6 3 [1; 0; 5; 2; -1; -2] |> should equal [3; 0; 15; 6; -3; -6]

[<Property>]
let multiplyBy_ImplementationsAreEquivalent (x: int) (list: int list) =
    multiplyBy'1 x list = multiplyBy'6 x list

Check.QuickThrowOnFailure multiplyBy_ImplementationsAreEquivalent
