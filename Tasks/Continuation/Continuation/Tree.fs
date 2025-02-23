namespace Continuation

module Tree =
    type Tree<'a> =
        | Empty
        | Node of 'a * Tree<'a> * Tree<'a>

    type ContinuationStep<'a> =
        | Done
        | Step of 'a * (unit -> ContinuationStep<'a>)

    let rec linearize tree cont =
        match tree with
        | Empty -> cont ()
        | Node (curr, left, right)
            -> Step (curr, (fun () -> linearize left (fun () -> linearize right cont)))
