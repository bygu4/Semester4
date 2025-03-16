module Computer

type OSName = string
type ComputerName = string

type InfectionChance = float
type IsInfected = bool

type OperatingSystem =
    | Windows
    | MacOS
    | Linux

let osName (os: OperatingSystem): OSName =
    match os with
    | Windows -> "Windows"
    | MacOS -> "MacOS"
    | Linux -> "Linux"

let infectionChance (os: OperatingSystem): InfectionChance =
    match os with
    | Windows -> 0.9
    | MacOS -> 0.5
    | Linux -> 0.1

type Computer (name: ComputerName, os: OperatingSystem, infected: IsInfected option) =
    let mutable isInfected = Option.defaultValue false infected

    member _.OS = os

    member _.IsInfected = isInfected

    member _.Infect () = isInfected <- true

    member _.Print () =
        printfn "%s (%s): %s" name (osName os) (if isInfected then "infected" else "safe")

type Computers = Computer list
