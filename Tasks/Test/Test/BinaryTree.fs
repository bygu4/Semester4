module BinaryTree

type Tree<'a> =
    | None
    | Tip of 'a * Tree<'a> * Tree<'a>

let filter (tree: Tree<'a>) (condition: 'a -> bool) =
    let rec filterInternal (tree: Tree<'a>) (condition: 'a -> bool) (cont: unit -> 'a list) =
        match tree with
        | None -> cont ()
        | Tip (x, left, right) ->
            if condition x then
                filterInternal left condition (fun () ->
                    x :: filterInternal right condition cont
                )
            else
                filterInternal left condition (fun () ->
                    filterInternal right condition cont
                )

    filterInternal tree condition (fun () -> [])
