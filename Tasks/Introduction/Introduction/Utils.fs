module Introduction

let factorial n =
    let rec factorialInternal n acc =
        match n with
        | 0 -> Ok acc
        | _ when n > 0 -> factorialInternal (n - 1) (acc * n)
        | _ -> Error "n should be non-negative"
    factorialInternal n 1

let fibonacci n =
    let rec fibonacciInternal n curr prev =
        match n with
        | 1 -> Ok prev
        | _ when n > 1 -> fibonacciInternal (n - 1) (prev + curr) curr
        | _ -> Error "n should be a natural number"
    fibonacciInternal n 1 1

let reverse target =
    let rec reverseInternal target acc =
        match target with
        | [] -> acc
        | head :: tail -> reverseInternal tail (head :: acc)
    reverseInternal target []

let findElement element list =
    let rec findInternal element list index =
        match list with
        | [] -> None
        | head :: _ when head = element -> Some index
        | _ :: tail -> findInternal element tail (index + 1)
    findInternal element list 0

let powersOfTwo n m =
    let rec powersInternal m acc =
        match m with
        | 0 -> Ok acc
        | _ when m > 0 -> powersInternal (m - 1) (List.head acc / 2.0 :: acc)
        | _ -> Error "m should be non-negative"
    powersInternal m [2.0 ** float (n + m)]
