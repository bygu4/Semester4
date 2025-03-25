namespace LocalNetwork

open System

type OperatingSystem(name: string, infectionChance: float) =
    let random = new Random (DateTime.Now.Ticks % int64 Int32.MaxValue |> int)

    member _.Name = name

    interface IOperatingSystem with
        member _.Name = name

        member _.CanBecomeInfected = infectionChance > 0.0

        member _.IsToBeInfected () = random.NextDouble () < infectionChance

        member this.CompareTo (other: IOperatingSystem) =
            this.Name.CompareTo other.Name
