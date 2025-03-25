namespace LocalNetwork

type IOperatingSystem =
    abstract member Name: string with get
    abstract member CanBecomeInfected: bool with get
    abstract member IsToBeInfected: unit -> bool
    abstract member CompareTo: IOperatingSystem -> int
