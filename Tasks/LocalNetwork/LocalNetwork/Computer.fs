namespace LocalNetwork

open System

/// A class representing a computer.
type Computer (name: string, os: IOperatingSystem, ?infected: bool) =
    let mutable isInfected = Option.defaultValue false infected

    /// Gets the name of the computer.
    member _.Name = name

    /// Gets the operating system of the computer.
    member _.OS = os

    /// Whether the computer is infected.
    member _.IsInfected = isInfected

    /// Make the computer infected.
    member _.Infect () = isInfected <- true

    /// Whether the computer can become infected.
    member _.CanBecomeInfected = os.CanBecomeInfected

    /// Whether to infect the computer on the next contact with the virus.
    member _.IsToBeInfected () = os.IsToBeInfected ()

    /// Print info about the computer to the console.
    member _.Print () =
        printfn "%s (%s): %s" name os.Name (if isInfected then "infected" else "safe")

    interface IComparable with
        /// Compare the computer with the given object.
        member this.CompareTo other =
            match other with
            | :? Computer as other ->
                let nameComparison = this.Name.CompareTo other.Name
                if nameComparison <> 0 then nameComparison
                else this.OS.CompareTo other.OS
            | _ -> 1
