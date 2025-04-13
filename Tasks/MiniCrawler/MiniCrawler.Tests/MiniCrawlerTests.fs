module MiniCrawler.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let testGetAllLinksInfo_NoLinks_GetEmptySequence () =
    getAllLinksInfo "https://github.com/login"
    |> Async.RunSynchronously
    |> should equal []

[<Test>]
let testGetAllLinksInfo_SingleLink_GetReferenceWithLength () =
    getAllLinksInfo "https://demo.cyotek.com/"
    |> Async.RunSynchronously
    |> should equal ["https://www.cyotek.com", 14352]

[<Test>]
let testGetAllLinksInfo_ManyLinks_GetCorrectNumberOfLinks () =
    let links =
        getAllLinksInfo "https://en.wikipedia.org/wiki/F_Sharp_(programming_language)"
        |> Async.RunSynchronously
        |> Seq.toList

    links.Length |> should equal 50
    links |> Seq.map snd |> Seq.exists (( >= ) 0) |> should be False
