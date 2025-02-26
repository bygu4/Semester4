module EvenNumbersTests

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck.NUnit

open EvenNumbers

[<Test>]
let testEvenNumbers_Map () =
    countEvenNumbers_Map [] |> should equal 0
    countEvenNumbers_Map [1; 5; -3; 5; 77; 9] |> should equal 0
    countEvenNumbers_Map [4; 6; 8; 8] |> should equal 4
    countEvenNumbers_Map [0; 0; 1; 2; 3] |> should equal 3
    countEvenNumbers_Map [56592; -321321; 898934; 90901; -137] |> should equal 2
    countEvenNumbers_Map [2147483647] |> should equal 0

[<Property>]
let areEquivalent_MapAndFilter (list: list<int>) =
    countEvenNumbers_Map list = countEvenNumbers_Filter list

[<Property>]
let areEquivalent_FilterAndFold (list: list<int>) =
    countEvenNumbers_Filter list = countEvenNumbers_Fold list

Check.QuickThrowOnFailure areEquivalent_MapAndFilter
Check.QuickThrowOnFailure areEquivalent_FilterAndFold
