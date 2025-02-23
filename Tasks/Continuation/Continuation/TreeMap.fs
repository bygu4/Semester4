namespace Continuation

module TreeMap =
    let rec map mapping tree =
        match tree with
        | Tree.Empty -> Tree.Empty
        | Tree.Node (x,  left, right)
            -> Tree.Node (mapping x, map mapping left, map mapping right)
