module TreeMap

type Tree<'a> =
    | Empty
    | Node of 'a * Tree<'a> * Tree<'a>

let rec map mapping tree =
    let rec mapInternal tree mapping cont =
        match tree with
        | Empty -> cont Empty
        | Node (value, left, right) ->
            mapInternal left mapping (fun mappedLeft ->
                mapInternal right mapping (fun mappedRight ->
                    Node (mapping value, mappedLeft, mappedRight) |> cont
                )
            )

    mapInternal tree mapping id
