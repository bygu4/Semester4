module TreeMapTests

open NUnit.Framework
open FsUnit

open TreeMap

[<Test>]
let testTreeMap () =
    let strTree =
        Node ("abc",
            Node ("", Empty, Node ("21321", Empty, Empty)),
            Node ("ololo332", Empty, Empty))
    let lenTree =
        Node (3,
            Node (0, Empty, Node (5, Empty, Empty)),
            Node (8, Empty, Empty))
    let intTree =
        Node (1, Empty, Node (2, Empty, Node (3, Empty, Node (4, Empty, Empty))))
    let squaresTree = 
        Node (1, Empty, Node (4, Empty, Node (9, Empty, Node (16, Empty, Empty))))

    map (( * ) 5) Empty |> should equal Tree<int>.Empty
    map (( = ) 0) (Node (0, Empty, Empty)) |> should equal (Node (true, Empty, Empty))
    map sqrt (Node (9.0, Empty, Empty)) |> should equal (Node (3.0, Empty, Empty))
    map String.length strTree |> should equal lenTree
    map (fun x -> x * x) intTree |> should equal squaresTree

[<Test>]
let testMapWithLargeTree () =
    let treeDepth = 1000000
    let sourceTree =
        { 1 .. treeDepth }
        |> Seq.fold (fun node _ -> Node (1, node, Empty)) Empty
    let resultTree =
        { 1 .. treeDepth }
        |> Seq.fold (fun node _ -> Node (8, node, Empty)) Empty

    sourceTree
    |> map (( + ) 7)
    |> areEqual resultTree
    |> should be True
