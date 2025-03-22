module Computer

open System
open OperatingSystem

type Computer (name: string, os: OperatingSystem, infected: bool option) =
    let mutable isInfected = Option.defaultValue false infected

    member _.Name = name

    member _.OS = os

    member _.IsInfected = isInfected

    member _.Infect () = isInfected <- true

    member _.CanBecomeInfected = os.CanBecomeInfected

    member this.ShouldBecomeInfected () = os.ShouldBecomeInfected ()

    member _.Print () =
        printfn "%s (%s): %s" name os.Name (if isInfected then "infected" else "safe")

    interface IComparable with
        member this.CompareTo other =
            match other with
            | :? Computer as other ->
                let nameComparison = this.Name.CompareTo other.Name
                if nameComparison <> 0 then nameComparison
                else this.OS.CompareTo other.OS
            | _ -> 1
