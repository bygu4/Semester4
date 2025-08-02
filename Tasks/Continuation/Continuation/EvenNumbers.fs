module EvenNumbers

let ( %% ) x y =
    let rem = x % y
    if rem >= 0 then rem else rem + y

let countEvenNumbers_Map: list<int> -> int =
    Seq.map (fun x -> (x + 1) %% 2) >> Seq.sum

let countEvenNumbers_Filter: list<int> -> int =
    Seq.filter (fun x -> x % 2 = 0) >> Seq.length

let countEvenNumbers_Fold: list<int> -> int =
    Seq.fold (fun acc x -> acc + (x + 1) %% 2) 0
