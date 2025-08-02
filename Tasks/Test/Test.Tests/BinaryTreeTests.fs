module BinaryTree.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let testFilter_EmptyTree_GetEmptyList () =
    filter None (fun x -> x > 0) |> _.IsEmpty |> should be True

[<Test>]
let testFilter_SingleElementTree_GetTheTip () =
    filter (Tip ("abc", None, None)) (fun s -> s.StartsWith "a")
    |> should equal ["abc"]

[<Test>]
let testFilter_NestedTree_GetEvenNumbers () =
    let tree = Tip (100,
        Tip (0,
            Tip (-1,
                None,
                Tip (15, None, None)
                ),
            Tip (-124, None, None)
            ),
        Tip (1000,
            Tip (-6, None, None),
            None
            )
        )
    filter tree (fun x -> x % 2 = 0)
    |> should equal [0; -124; 100; -6; 1000]
