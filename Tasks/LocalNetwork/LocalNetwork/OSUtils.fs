namespace LocalNetwork

/// Utility for managing operating systems.
module OSUtils =
    /// Some predefined operating systems.
    let windows () = new OperatingSystem ("Windows", 0.9)
    let macOs () = new OperatingSystem ("MacOS", 0.5)
    let linux () = new OperatingSystem ("Linux", 0.1)

    /// Get a predefined OS by its name.
    let getOs osName =
        match osName with
        | "windows" -> Some (windows ())
        | "macOs" -> Some (macOs ())
        | "linux" -> Some (linux ())
        | _ -> None
