module Queue.Tests

open NUnit.Framework
open FsUnit
open System

[<Test>]
let testEnqueue_AddElements_SizeShouldBeCorrect () =
    let queue = new Queue<int> ()
    queue.Count |> should equal 0
    for x in { 1 .. 100 } do
        queue.Enqueue x
    queue.Count |> should equal 100

[<Test>]
let testDequeue_AddEndGetElements_ObtainElementsInOrderOfAddition () =
    let queue = new Queue<int> ()
    queue.Enqueue -100
    queue.Enqueue 42
    queue.Enqueue 0
    queue.Enqueue Int32.MaxValue
    queue.Enqueue Int32.MinValue
    queue.Dequeue () |> should equal -100
    queue.Dequeue () |> should equal 42
    queue.Dequeue () |> should equal 0
    queue.Count |> should equal 2
    queue.Dequeue () |> should equal Int32.MaxValue
    queue.Dequeue () |> should equal Int32.MinValue
    queue.Count |> should equal 0

[<Test>]
let testDequeue_GetFromEmptyQueue_ThrowException () =
    let queue = new Queue<string> ()
    (fun () -> queue.Dequeue () |> ignore) |> should throw typeof<InvalidOperationException>
    queue.Count |> should equal 0

[<Test>]
let testDequeue_RemoveAllElementsAndGetFromEmpty_ThrowException () =
    let queue = new Queue<float> ()
    queue.Enqueue 10.0
    queue.Enqueue 12.2
    queue.Dequeue () |> should equal 10.0
    queue.Dequeue () |> should equal 12.2
    (fun () -> queue.Dequeue () |> ignore) |> should throw typeof<InvalidOperationException>
    queue.Count |> should equal 0

[<Test>]
let testDequeue_RemoveElementsAndAddNewOnes_ElementsAreAdded () =
    let queue = new Queue<string> ()
    queue.Enqueue "trololo"
    queue.Enqueue ""
    queue.Dequeue () |> should equal "trololo"
    queue.Dequeue () |> should equal ""
    queue.Count |> should equal 0
    queue.Enqueue "test"
    queue.Enqueue "a"
    queue.Count |> should equal 2
    queue.Dequeue () |> should equal "test"
    queue.Dequeue () |> should equal "a"
