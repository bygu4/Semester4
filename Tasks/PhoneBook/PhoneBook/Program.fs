open System

open PhoneBook

printf "
    Phone Book
    ----------
    Commands:
        exit: exit the app
        add {name} {number}: add a record to the book
        find number {name}: find a phone number by the given name
        find name {number}: find a name by the given phone number
        get all: print all of the records in the book
        save to {filePath}: save records to the given file
        read from {filePath}: read records from the given file\n
"

let getCommand () =
    let input = Console.ReadLine()
    let words = input.Split ' '
    match words with
    | [| "exit" |] -> Some Exit
    | [| "add"; name; number |] -> Some (AddRecord (name, number))
    | [| "find"; "number"; name |] -> Some (FindPhoneNumber name)
    | [| "find"; "name"; number |] -> Some (FindName number)
    | [| "get"; "all" |] -> Some GetAll
    | [| "save"; "to"; filePath |] -> Some (SaveToFile filePath)
    | [| "read"; "from"; filePath |] -> Some (ReadFromFile filePath)
    | _ -> None

let handleOutput output =
    match output with
    | Message msg -> printfn "%s\n" msg
    | FoundName (Some value) | FoundPhoneNumber (Some value) -> printfn "%s\n" value
    | FoundName None -> printf "record with such number was not found\n"
    | FoundPhoneNumber None -> printf "record with such name was not found\n"
    | All records ->
        for record in records do
            printfn "%s %s\n" <|| record

let rec run phoneBook =
    printf "-> "
    let command = getCommand()
    match command with
    | Some command ->
        let output, phoneBook = execute command phoneBook
        handleOutput output
        run phoneBook
    | None ->
        printf "Unrecognized command\n"
        run phoneBook

run []
