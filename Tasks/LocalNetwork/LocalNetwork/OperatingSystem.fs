module OperatingSystem

open System

type OperatingSystem(name: string, infectionChance: float) =
    let random = new Random (DateTime.Now.Ticks % int64 Int32.MaxValue |> int)

    member _.Name = name

    member _.CanBecomeInfected = infectionChance > 0

    member _.ShouldBecomeInfected () = random.NextDouble () < infectionChance

    member this.CompareTo (other: OperatingSystem) =
        this.Name.CompareTo other.Name
