open System

open OperatingSystem
open Computer
open Network

let splitOptions = StringSplitOptions.TrimEntries + StringSplitOptions.RemoveEmptyEntries

let rec readNumberOfComputers () =
    printf "Enter the number of computers: "
    let input = Console.ReadLine ()
    let number = 0
    let isParsed = Int32.TryParse (input, ref number)
    if isParsed then number
    else
        printfn "Unrecognized number, try again"
        readNumberOfComputers ()

let rec readComputer number =
    printf "%d: " number
    let input = Console.ReadLine ()
    let words = input.Split (' ', splitOptions)
    match words with
    | [| osName |] ->
        let os = getOs osName
        match os with
        | Some os -> new Computer (number.ToString (), os)
        | None ->
            printfn "Unrecognized operating system, try again"
            readComputer number
    | [| osName; "infected" |] ->
        let os = getOs osName
        match os with
        | Some os -> new Computer (number.ToString (), os, true)
        | None ->
            printfn "Unrecognized operating system, try again"
            readComputer number
    | _ ->
        printfn "Invalid syntax, try again"
        readComputer number

let readComputers number =
    { 1 .. (number + 1) }
    |> Seq.map readComputer
    |> Seq.toArray

let rec readLink (computers: Computer array) =
    let input = Console.ReadLine ()
    let words = input.Split (' ', splitOptions)
    match words with
    | [| word1; word2 |] ->
        let number1 = 0
        let number2 = 0
        let areParsed = Int32.TryParse(word1, ref number1) && Int32.TryParse(word2, ref number2)
        let areInRange =
            areParsed
            && 1 <= number1 && number1 <= computers.Length
            && 1 <= number2 && number2 <= computers.Length
        if areInRange then Some (computers[number1 - 1], computers[number2 - 1])
        else readLink computers
    | [| "done" |] -> None
    | _ -> readLink computers

let rec readLinks (computers: Computer array) (acc: Link Set) =
    let nextLink = readLink computers
    match nextLink with
    | Some link -> readLinks computers (acc.Add link)
    | None -> acc

let rec run (network: Network) =
    if network.CanChange then
        network.Print ()
        run network

let numberOfComputers = readNumberOfComputers ()
let computers = readComputers numberOfComputers
let links = readLinks computers (set [])
let network = new Network (computers, links)

run network
