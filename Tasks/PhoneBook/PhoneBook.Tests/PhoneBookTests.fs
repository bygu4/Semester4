module PhoneBook.Tests

open System.IO

open NUnit.Framework
open FsUnit

let testFilesDir = "TestFiles"
let correctBookPath = Path.Join [| testFilesDir; "CorrectBook.txt" |]
let incorrectBookPath = Path.Join [| testFilesDir; "IncorrectBook.txt" |]
let emptyBookPath = Path.Join [| testFilesDir; "EmptyBook.txt" |]

let testRecords =
    [
        "test", "1234567890";
        "ololo", "ololo";
        "abc", "abc";
        "qwerty", "";
        "abc", "def";
    ]

[<Test>]
let testAddRecord () =
    let phoneBook = testRecords
    let output, phoneBook = execute phoneBook <| AddRecord ("some name", "some number")
    output |> should equal (Message "Record was added")
    phoneBook |> should equal (Record ("some name", "some number") :: testRecords)

[<Test>]
let testFindPhoneNumber () =
    let phoneBook = testRecords
    let testCase toFind found =
        let output, phoneBook = execute phoneBook <| FindPhoneNumber toFind
        output |> should equal (FoundPhoneNumber found)
        phoneBook |> should equal testRecords

    testCase "test" (Some "1234567890")
    testCase "abc" (Some "abc")
    testCase "qwerty" (Some "")
    testCase "" None
    testCase "some other name" None

[<Test>]
let testFindName () =
    let phoneBook = testRecords
    let testCase toFind found =
        let output, phoneBook = execute phoneBook <| FindName toFind
        output |> should equal (FoundName found)
        phoneBook |> should equal testRecords

    testCase "1234567890" (Some "test")
    testCase "abc" (Some "abc")
    testCase "" (Some "qwerty")
    testCase "def" (Some "abc")
    testCase "some other number" None

[<Test>]
let testGetAll () =
    let phoneBook = testRecords
    let output, phoneBook = execute phoneBook <| GetAll
    output |> should equal (All testRecords)
    phoneBook |> should equal testRecords

[<Test>]
let testSaveToFile () =
    let phoneBook = testRecords
    Directory.CreateDirectory "tmp" |> ignore

    let output, phoneBook = execute phoneBook <| SaveToFile "tmp/book.txt"
    output |> should equal (Message "Phone book was saved successfully")
    phoneBook |> should equal testRecords

    let output, phoneBook = execute [] <| ReadFromFile "tmp/book.txt"
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal testRecords

    (fun () -> SaveToFile "qwert/yuiop" |> execute phoneBook |> ignore)
    |> should throw typeof<DirectoryNotFoundException>

[<Test>]
let testReadFromFile () =
    let output, phoneBook = execute [] <| ReadFromFile correctBookPath
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal testRecords

    let output, phoneBook = execute ["some", "record"] <| ReadFromFile emptyBookPath
    output |> should equal (Message "Phone book was read successfully")
    phoneBook |> should equal ([]: PhoneBook)

    (fun () -> ReadFromFile incorrectBookPath |> execute [] |> ignore)
    |> should throw typeof<InvalidDataException>

    (fun () -> ReadFromFile "non_existing_file.txt" |> execute [] |> ignore)
    |> should throw typeof<FileNotFoundException>

    (fun () -> ReadFromFile "qwert/yuiop" |> execute [] |> ignore)
    |> should throw typeof<DirectoryNotFoundException>
