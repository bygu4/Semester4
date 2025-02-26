module TreeMap

type Tree<'a> =
    | Empty
    | Node of 'a * Tree<'a> * Tree<'a>

let rec map mapping tree =
    match tree with
    | Empty -> Empty
    | Node (value, left, right) ->
        Node (mapping value, map mapping left, map mapping right)
