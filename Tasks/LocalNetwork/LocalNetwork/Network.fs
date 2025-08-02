namespace LocalNetwork

/// A link between two computers.
type Link = Computer * Computer

/// A class representing a local network of computers.
type Network (computers: Computer seq, links: Link Set) =
    let mutable canChange = true

    /// Whether the state of the network can change.
    member _.CanChange = canChange

    /// Perform a step of the network simulation.
    member _.ToNextIteration () =
        canChange <- false
        let mutable toInfect = []

        for link in links do
            match link with
            | comp1, comp2 when comp1.IsInfected && not comp2.IsInfected ->
                if comp2.CanBecomeInfected then canChange <- true
                if comp2.IsToBeInfected () then toInfect <- comp2 :: toInfect
            | comp1, comp2 when not comp1.IsInfected && comp2.IsInfected ->
                if comp1.CanBecomeInfected then canChange <- true
                if comp1.IsToBeInfected () then toInfect <- comp1 :: toInfect
            | _ -> ()

        for comp in toInfect do
            comp.Infect ()

    /// Print info about the network to the console.
    member _.Print () =
        for computer in computers do
            computer.Print ()
