module EvenNumbers

let countEvenNumbers1: list<int> -> int =
    Seq.map (fun x -> (x + 1) % 2) >> Seq.sum

let countEvenNumbers2: list<int> -> int =
    Seq.filter (fun x -> x % 2 = 0) >> Seq.length

let countEvenNumbers3: list<int> -> int =
    Seq.fold (fun acc x -> acc + (x + 1) % 2) 0
