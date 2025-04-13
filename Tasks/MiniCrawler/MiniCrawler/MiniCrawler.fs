module MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

let referencePattern = @"(""http(s?)://[^""]*"")|('http(s?)://[^']*')"
let linkPattern = "<a href=" + referencePattern + ">"

/// Gets html content of the resource with the given `address` as a string.
let getContent (address: string) =
    task {
        use client = new HttpClient()
        return! client.GetStringAsync address
    } |> Async.AwaitTask

/// Asynchronously gets html content for each of the pages defined by given `addresses`.
let getAllContents (addresses: string seq) =
    addresses
    |> Seq.map getContent
    |> Async.Parallel

/// Gets all page references in the given html `content`.
let getAllReferences (content: string) =
    Regex.Matches(content, linkPattern)
    |> Seq.map (fun link -> Regex.Match(link.Value, referencePattern))
    |> Seq.map (fun href -> href.Value.Substring(1, href.Length - 2))

/// Gets info about each of the resource referenced by the page with the given `address`.
/// Info contains the reference address and the length of the referenced page.
let getAllLinksInfo (address: string) =
    async {
        let! content = getContent address
        let references = getAllReferences content
        let! referenceContents = getAllContents references
        let lengths = referenceContents |> Seq.map _.Length
        return Seq.zip references lengths
    }

/// Prints info about each of the resource referenced by the page with the given `address`.
/// Info contains the reference address and the length of the referenced page.
let printAllLinksInfo (address: string) =
    let printInfo (address: string, length: int) =
        printfn "%s --- %d" address length

    getAllLinksInfo address
    |> Async.RunSynchronously
    |> Seq.map printInfo
    |> ignore
