module PointFree

let multiplyBy'1 x list = List.map (fun y -> y * x) list

let multiplyBy'2 x list = List.map (fun y -> x * y) list

let multiplyBy'3 x list = List.map (( * ) x) list

let multiplyBy'4 x = List.map (( * ) x)

let multiplyBy'5 x = ( * ) x |> List.map

let multiplyBy'6 = ( * ) >> List.map
