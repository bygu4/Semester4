module TreeMap

type Tree<'a> =
    | Empty
    | Node of 'a * Tree<'a> * Tree<'a>

[<TailCall>]
let rec private mapCPS mapping tree cont =
    match tree with
    | Empty -> cont Empty
    | Node (value, left, right) ->
        mapCPS mapping left (fun mappedLeft ->
            mapCPS mapping right (fun mappedRight ->
                Node (mapping value, mappedLeft, mappedRight) |> cont
            )
        )

let map mapping tree = mapCPS mapping tree id

[<TailCall>]
let rec private areEqualCPS tree1 tree2 cont =
    match tree1, tree2 with
    | Empty, Empty -> cont true
    | Empty, Node _ | Node _, Empty -> cont false
    | Node (val1, _, _), Node (val2, _, _) when val1 <> val2 -> cont false
    | Node (_, left1, right1), Node (_, left2, right2) ->
        areEqualCPS left1 left2 (fun leftAreEqual ->
            areEqualCPS right1 right2 (fun rightAreEqual ->
                leftAreEqual && rightAreEqual |> cont
            )
        )

let areEqual tree1 tree2 = areEqualCPS tree1 tree2 id
