module PhoneBook

open System.IO

type Name = Name of string
type PhoneNumber = PhoneNumber of string
type FilePath = FilePath of string

/// A record to store in the phone book.
type Record = Name * PhoneNumber
type Records = Record list
type PhoneBook = Book of Records

/// A command to be interpreted.
type Command =
    | Exit
    | AddRecord of Record
    | FindPhoneNumber of Name
    | FindName of PhoneNumber
    | GetAll
    | SaveToFile of FilePath
    | ReadFromFile of FilePath

/// An output to yield after the command execution.
type Output =
    | Message of string
    | FoundPhoneNumber of PhoneNumber option
    | FoundName of Name option
    | All of Records

/// Get `nth` item of the `option` tuple.
let unwrap nth option =
    match option with
    | None -> None
    | Some tuple -> Some (nth tuple)

/// Save the given `phoneBook` to a file at the given `filePath`.
let saveToFile (FilePath filePath) (Book records) =
    use writer = new StreamWriter (filePath)
    for Name name, PhoneNumber number in records do
        writer.WriteLine (name + " " + number)

/// Read a phone book from a file at the given `filePath`.
let readFromFile (FilePath filePath) =
    File.ReadAllLines filePath
    |> Array.toList
    |> List.map (fun line ->
        let words = line.Split ' '
        match words with
        | [| name; number |] -> Record (Name name, PhoneNumber number)
        | _ -> raise (InvalidDataException "Invalid record format"))
    |> Book

/// Execute the given `command` on the given `phoneBook`.
let execute (Book records as phoneBook) (command: Command) =
    match command with
    | Exit -> exit 0
    | AddRecord record -> Message "Record was added", Book (record :: records)
    | FindPhoneNumber name ->
        FoundPhoneNumber (List.tryFind (fst >> ( = ) name) records |> unwrap snd), phoneBook
    | FindName number ->
        FoundName (List.tryFind (snd >> ( = ) number) records |> unwrap fst), phoneBook
    | GetAll -> All records, phoneBook
    | SaveToFile filePath ->
        saveToFile filePath phoneBook
        Message "Phone book was saved successfully", phoneBook
    | ReadFromFile filePath -> Message "Phone book was read successfully", readFromFile filePath
