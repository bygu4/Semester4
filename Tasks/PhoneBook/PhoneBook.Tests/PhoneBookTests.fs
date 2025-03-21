module PhoneBook.Tests

open System.IO

open NUnit.Framework
open FsUnit

let testFilesDir = "TestFiles"
let correctBookPath = Path.Join [| testFilesDir; "CorrectBook.txt" |] |> FilePath
let incorrectBookPath = Path.Join [| testFilesDir; "IncorrectBook.txt" |] |> FilePath
let emptyBookPath = Path.Join [| testFilesDir; "EmptyBook.txt" |] |> FilePath

let testRecords =
    [
        "test", "1234567890";
        "ololo", "ololo";
        "abc", "abc";
        "qwerty", "";
        "abc", "def";
    ] |> List.map (fun (name, number) -> Name name, PhoneNumber number)

[<Test>]
let testAddRecord () =
    let phoneBook = Book testRecords
    let output, phoneBook = execute phoneBook <| AddRecord (Name "some name", PhoneNumber "some number")
    output |> should equal (Message "Record was added")
    phoneBook |> should equal (Book (Record (Name "some name", PhoneNumber "some number") :: testRecords))

[<Test>]
let testFindPhoneNumber () =
    let phoneBook = Book testRecords
    let testCase toFind found =
        let output, phoneBook = execute phoneBook <| FindPhoneNumber (Name toFind)
        output |> should equal (FoundPhoneNumber found)
        phoneBook |> should equal (Book testRecords)

    testCase "test" (Some (PhoneNumber "1234567890"))
    testCase "abc" (Some (PhoneNumber "abc"))
    testCase "qwerty" (Some (PhoneNumber ""))
    testCase "" None
    testCase "some other name" None

[<Test>]
let testFindName () =
    let phoneBook = Book testRecords
    let testCase toFind found =
        let output, phoneBook = execute phoneBook <| FindName (PhoneNumber toFind)
        output |> should equal (FoundName found)
        phoneBook |> should equal (Book testRecords)

    testCase "1234567890" (Some (Name "test"))
    testCase "abc" (Some (Name "abc"))
    testCase "" (Some (Name "qwerty"))
    testCase "def" (Some (Name "abc"))
    testCase "some other number" None

[<Test>]
let testGetAll () =
    let phoneBook = Book testRecords
    let output, phoneBook = execute phoneBook <| GetAll
    output |> should equal (All testRecords)
    phoneBook |> should equal (Book testRecords)

    let output, phoneBook = execute (Book []) <| GetAll
    output |> should equal (All [])
    phoneBook |> should equal (Book [])

[<Test>]
let testSaveToFile () =
    let phoneBook = Book testRecords
    Directory.CreateDirectory "tmp" |> ignore

    let output, phoneBook = execute phoneBook <| SaveToFile (FilePath "tmp/book.txt")
    output |> should equal (Message "Phone book was saved successfully")
    phoneBook |> should equal (Book testRecords)

    let output, phoneBook = execute (Book []) <| ReadFromFile (FilePath "tmp/book.txt")
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal (Book testRecords)

    (fun () -> SaveToFile (FilePath "qwert/yuiop") |> execute phoneBook |> ignore)
    |> should throw typeof<DirectoryNotFoundException>

[<Test>]
let testReadFromFile () =
    let output, phoneBook = execute (Book []) <| ReadFromFile correctBookPath
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal (Book testRecords)

    let output, phoneBook = execute (Book [Name "some", PhoneNumber "record"]) <| ReadFromFile emptyBookPath
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal (Book [])

    (fun () -> ReadFromFile incorrectBookPath |> execute (Book []) |> ignore)
    |> should throw typeof<InvalidDataException>

    (fun () -> ReadFromFile (FilePath "non_existing_file.txt") |> execute (Book []) |> ignore)
    |> should throw typeof<FileNotFoundException>

    (fun () -> ReadFromFile (FilePath "qwert/yuiop") |> execute (Book []) |> ignore)
    |> should throw typeof<DirectoryNotFoundException>
