namespace Queue

open System

/// Class representing an element in a linked list.
type Node<'a>(value: 'a, next: Node<'a> option) =
    let mutable value = value
    let mutable next = next

    /// Get the value of the node.
    member _.Value () = value

    /// Get the next node of the current one.
    member _.Next () = next

    /// Set the next node of the current one.
    member _.SetNext (node: Node<'a> option) = next <- node

/// Class representing a generic queue.
type Queue<'a>() =
    let mutable head: Node<'a> option = None
    let mutable tail: Node<'a> option = None

    /// Add the given value to the queue.
    member _.Enqueue (value: 'a): unit =
        let next = Node (value, None)
        match tail with
        | Some node ->
            node.SetNext <| Some next
            tail <- Some node
        | None ->
            head <- Some next
            tail <- Some next

    /// Remove the first element from the queue and return its value.
    member _.Dequeue (): 'a =
        match head with
        | Some node ->
            let res = node.Value ()
            head <- node.Next ()
            if head.IsNone then tail <- None
            res
        | None ->
            raise (new InvalidOperationException "Error on dequeue: queue was empty")
