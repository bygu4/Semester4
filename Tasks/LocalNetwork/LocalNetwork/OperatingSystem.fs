namespace LocalNetwork

open System

/// An implementation of an operating system of a computer.
type OperatingSystem(name: string, infectionChance: float) =
    let random = new Random (DateTime.Now.Ticks % int64 Int32.MaxValue |> int)

    /// Gets the name of the operating system.
    member _.Name = name

    interface IOperatingSystem with
        /// Gets the name of the operating system.
        member _.Name = name

        /// Whether a computer with this OS can become infected.
        member _.CanBecomeInfected = infectionChance > 0.0

        /// Whether to infect a computer with this OS on the next contact with the virus.
        member _.IsToBeInfected () = random.NextDouble () < infectionChance

        /// Compare this OS with the given one.
        member this.CompareTo (other: IOperatingSystem) =
            this.Name.CompareTo other.Name
