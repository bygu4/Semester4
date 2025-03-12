open System

open PhoneBook

let printInfo () =
    printf "
Phone Book
----------
Commands:
    exit\t\t\t exit the app
    add {name} {number}\t\t add a record to the book
    find number {name}\t\t find a phone number by the given name
    find name {number}\t\t find a name by the given phone number
    get all\t\t\t print all of the records in the book
    save to {filePath}\t\t save records to the given file
    read from {filePath}\t read records from the given file
"

let getCommand () =
    let input = Console.ReadLine()
    let splitOptions = StringSplitOptions.TrimEntries + StringSplitOptions.RemoveEmptyEntries
    let words = input.Split (' ', splitOptions)
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
    | Message msg -> printfn "%s" msg
    | FoundName (Some value) | FoundPhoneNumber (Some value) -> printfn "%s" value
    | FoundName None -> printfn "Record with such number was not found"
    | FoundPhoneNumber None -> printfn "Record with such name was not found"
    | All records ->
        if List.isEmpty records then
            printfn "Phone book is empty"
        for record in records do
            printfn "%s %s" <|| record

let rec run phoneBook =
    printf "\n-> "
    let command = getCommand()
    match command with
    | Some command ->
        let output, phoneBook = execute command phoneBook
        handleOutput output
        run phoneBook
    | None ->
        printfn "Unrecognized command"
        run phoneBook

printInfo ()
run []
