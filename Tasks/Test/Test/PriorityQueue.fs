module PriorityQueue

open System

type Queue<'a>() =
    let mutable elements: ('a * int) list = []

    member _.Enqueue (element: 'a, priority: int) =
        elements <- (element, priority) :: elements

    member _.Dequeue () =
        if elements.IsEmpty then raise (new InvalidOperationException "queue was empty")
