module TreeMap

type Tree<'a> =
    | Empty
    | Node of 'a * Tree<'a> * Tree<'a>

let map mapping tree =

    let rec map mapping tree cont =
        match tree with
        | Empty -> cont Empty
        | Node (value, left, right) ->
            map mapping left (fun mappedLeft ->
                map mapping right (fun mappedRight ->
                    Node (mapping value, mappedLeft, mappedRight) |> cont
                )
            )

    in map mapping tree id
