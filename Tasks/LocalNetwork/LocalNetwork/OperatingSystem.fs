module OperatingSystem

open System

type OperatingSystem(name: string, infectionChance: float) =
    let random = new Random (DateTime.Now.Ticks % int64 Int32.MaxValue |> int)

    member _.Name = name

    member _.CanBecomeInfected = infectionChance > 0.0

    member _.IsToBeInfected () = random.NextDouble () < infectionChance

    member this.CompareTo (other: OperatingSystem) =
        this.Name.CompareTo other.Name

let windows () = new OperatingSystem ("Windows", 0.9)
let macOs () = new OperatingSystem ("MacOS", 0.5)
let linux () = new OperatingSystem ("Linux", 0.1)

let getOs osName =
    match osName with
    | "windows" -> Some (windows ())
    | "macOs" -> Some (macOs ())
    | "linux" -> Some (linux ())
    | _ -> None
