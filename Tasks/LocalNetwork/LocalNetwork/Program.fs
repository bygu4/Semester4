open System

open LocalNetwork
open OSUtils

let splitOptions = StringSplitOptions.TrimEntries + StringSplitOptions.RemoveEmptyEntries

let printInfo () =
    printf "
LocalNetwork
------------
A simple local network emulator.

Network consists of computers, every computer has a chance of becoming infected that depends on the operating system.

Available operating systems:
    Windows: 0.9 chance of infection
    MacOS: 0.5 chance of infection
    Linux: 0.1 chance of infection
"

let parseInt (input: string) =
    match Int32.TryParse input with
    | true, number -> Some number
    | _ -> None

let rec readNumberOfComputers () =
    printf "\nEnter the number of computers: "
    let input = Console.ReadLine ()
    match parseInt input with
    | Some number -> number
    | _ ->
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
            printfn "Unrecognized operating system, try again\n"
            readComputer number
    | [| osName; "infected" |] ->
        let os = getOs osName
        match os with
        | Some os -> new Computer (number.ToString (), os, true)
        | None ->
            printfn "Unrecognized operating system, try again\n"
            readComputer number
    | _ ->
        printfn "Invalid syntax, try again\n"
        readComputer number

let readComputers number =
    printfn "\nEnter the computers in format: {'linux' | 'macOs' | 'windows'} {?'infected'}"
    { 1 .. number }
    |> Seq.map readComputer
    |> Seq.toArray

let rec readLink (computers: Computer array) =
    let input = Console.ReadLine ()
    let words = input.Split (' ', splitOptions)
    match words with
    | [| word1; word2 |] ->
        let number1 = parseInt word1
        let number2 = parseInt word2
        match number1, number2 with
        | Some number1, Some number2 ->
            let areInRange =
                1 <= number1 && number1 <= computers.Length &&
                1 <= number2 && number2 <= computers.Length
            if areInRange then Some (computers[number1 - 1], computers[number2 - 1])
            else
                printfn "There are no computers with such number, try again\n"
                readLink computers
        | _ ->
            printfn "Unrecognized number, try again\n"
            readLink computers
    | [| "done" |] -> None
    | _ ->
        printfn "Invalid syntax, try again\n"
        readLink computers

let readLinks (computers: Computer array) =
    let rec readLinksInternal (computers: Computer array) (acc: Link Set) =
        let nextLink = readLink computers
        match nextLink with
        | Some link -> readLinksInternal computers (acc.Add link)
        | None -> acc

    printfn "\nEnter links between computers in format: {first computer number} {second computer number}"
    printfn "Enter 'done' if you want to stop"
    readLinksInternal computers (set [])

let rec run (network: Network) =
    printf "\n"
    network.Print ()
    if network.CanChange then
        network.ToNextIteration ()
        run network

printInfo ()
let numberOfComputers = readNumberOfComputers ()
let computers = readComputers numberOfComputers
let links = readLinks computers
let network = new Network (computers, links)

run network
