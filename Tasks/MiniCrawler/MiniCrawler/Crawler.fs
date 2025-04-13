module Crawler

open System.Net.Http
open System.Text.RegularExpressions

let referencePattern = @"http(s?)://(.*)"
let quotedReferencePattern = "\"" + referencePattern + "\""
let linkPattern = "<a href=" + quotedReferencePattern + ">"

/// Gets html content of the resource with the given `address` as a string.
let getContent (address: string) =
    task {
        use client = new HttpClient()
        return! client.GetStringAsync address
    } |> Async.AwaitTask

/// Asynchronously gets html content for each of the pages defined by given `addresses`.
let getContents (addresses: string seq) =
    addresses
    |> Seq.map getContent
    |> Async.Parallel

/// Gets all page references in the given html `content`.
let getAllReferences (content: string) =
    Regex.Matches(content, linkPattern)
    |> Seq.map _.ToString()
    |> Seq.map (fun link -> Regex.Match(link, referencePattern))
    |> Seq.map _.ToString()

/// Gets info about each of the resource referenced by the page with the given `address`.
/// Info contains the reference address and the length of the referenced page.
let getAllReferencedContents (address: string) =
    async {
        let! content = getContent address
        let references = getAllReferences content
        let! referenceContents = getContents references
        let lengths = referenceContents |> Seq.map _.Length
        return Seq.zip references lengths
    }

/// Prints info about each of the resource referenced by the page with the given `address`.
/// Info contains the reference address and the length of the referenced page.
let printAllLinksInfo (address: string) =
    let printInfo (address: string, length: int) =
        printfn "%s --- %d" address length

    getAllReferencedContents address
    |> Async.RunSynchronously
    |> Seq.map printInfo
    |> ignore
