module PhoneBook

open System.IO

type Name = string
type PhoneNumber = string
type FilePath = string

type Record = Name * PhoneNumber
type Records = Record list
type PhoneBook = Records

type Command =
    | Exit
    | AddRecord of Record
    | FindPhoneNumber of Name
    | FindName of PhoneNumber
    | GetAll
    | SaveToFile of FilePath
    | ReadFromFile of FilePath

type Output =
    | Message of string
    | FoundPhoneNumber of PhoneNumber option
    | FoundName of Name option
    | All of Records

let unwrap nth option =
    match option with
    | None -> None
    | Some tuple -> Some (nth tuple)

let saveToFile (filePath: string) (phoneBook: PhoneBook) =
    use writer = new StreamWriter (filePath)
    for name, number in phoneBook do
        writer.WriteLine (name + " " + number)

let readFromFile (filePath: string) =
    File.ReadAllLines filePath
    |> Array.toList
    |> List.map (fun line ->
        let words = line.Split ' '
        match words with
        | [| name; number|] -> Record (name, number)
        | _ -> raise (InvalidDataException "Invalid record format")
    )

let execute (command: Command) (phoneBook: PhoneBook) =
    match command with
    | Exit -> exit 0
    | AddRecord record -> Message "Record was added", record :: phoneBook
    | FindPhoneNumber name ->
        FoundPhoneNumber (List.tryFind (fst >> ( = ) name) phoneBook |> unwrap snd), phoneBook
    | FindName number ->
        FoundName (List.tryFind (snd >> ( = ) number) phoneBook |> unwrap fst), phoneBook
    | GetAll -> All phoneBook, phoneBook
    | SaveToFile filePath ->
        saveToFile filePath phoneBook
        Message "Phone book was written successfully", phoneBook
    | ReadFromFile filePath -> Message "Phone book was read successfully", readFromFile filePath
