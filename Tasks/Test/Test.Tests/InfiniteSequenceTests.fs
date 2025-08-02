module InfiniteSequence.Tests

open NUnit.Framework
open FsUnit
open FsCheck
open FsCheck.NUnit

[<Property>]
let anyEvenIndexOfSequenceIsPositive (index: NonNegativeInt) =
    match index with
    | x when int x % 2 = 0 -> Seq.item (int x) signAlternatingSequence > 0
    | x -> Seq.item (int x) signAlternatingSequence < 0

[<Test>]
let testSequence_GetFirstNItems () =
    signAlternatingSequence
    |> Seq.take 12
    |> should equal (seq [1; -2; 3; -4; 5; -6; 7; -8; 9; -10; 11; -12])

Check.QuickThrowOnFailure anyEvenIndexOfSequenceIsPositive
