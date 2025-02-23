namespace Continuation

module EvenNumbers =
    let countEvenNumbers1 () = Seq.map (fun x -> (x + 1) % 2) >> Seq.sum

    let countEvenNumbers2 () = Seq.filter (fun x -> x % 2 = 0) >> Seq.length

    let countEvenNumbers3 () = Seq.fold (fun acc x -> acc + (x + 1) % 2) 0
