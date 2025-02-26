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

    map (fun x -> x * 5) Empty |> should equal Tree<int>.Empty
    map sqrt (Node (9.0, Empty, Empty)) |> should equal (Node (3.0, Empty, Empty))
    map String.length strTree |> should equal lenTree
    map (fun x -> x * x) intTree |> should equal squaresTree
