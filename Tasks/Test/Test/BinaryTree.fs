module BinaryTree

/// Definition of a binary tree.
type Tree<'a> =
    | None
    | Tip of 'a * Tree<'a> * Tree<'a>

/// Get all of the elements of the given `tree` for which the `condition` is true.
let filter (tree: Tree<'a>) (condition: 'a -> bool) =

    /// Traverse the `tree` in CPS.
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
