namespace LocalNetwork

module OSUtils =
    let windows () = new OperatingSystem ("Windows", 0.9)
    let macOs () = new OperatingSystem ("MacOS", 0.5)
    let linux () = new OperatingSystem ("Linux", 0.1)

    let getOs osName =
        match osName with
        | "windows" -> Some (windows ())
        | "macOs" -> Some (macOs ())
        | "linux" -> Some (linux ())
        | _ -> None

