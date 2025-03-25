namespace LocalNetwork

/// An interface representing an operating system of a computer.
type IOperatingSystem =
    /// Gets the name of the operating system.
    abstract member Name: string with get

    /// Whether a computer with this OS can become infected.
    abstract member CanBecomeInfected: bool with get

    /// Whether to infect a computer with this OS on the next contact with the virus.
    abstract member IsToBeInfected: unit -> bool

    /// Compare this OS with the given one.
    abstract member CompareTo: IOperatingSystem -> int
