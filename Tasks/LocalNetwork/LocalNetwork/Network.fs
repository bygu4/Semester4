module Network

open Computer

type Link = Computer * Computer

type Network (computers: Computer seq, links: Link Set) =
    let mutable canChange = true

    member _.CanChange = canChange

    member _.ToNextIteration () =
        canChange <- false
        links
        |> Seq.map (fun link ->
            match link with
            | comp1, comp2 when comp1.IsInfected && not comp2.IsInfected ->
                if comp2.CanBecomeInfected then canChange <- true
                if comp2.ShouldBecomeInfected () then Some comp2
                else None
            | comp1, comp2 when not comp1.IsInfected && comp2.IsInfected ->
                if comp1.CanBecomeInfected then canChange <- true
                if comp1.ShouldBecomeInfected () then Some comp1
                else None
            | _ -> None)
        |> Seq.map (fun comp ->
            match comp with
            | Some comp -> comp.Infect ()
            | _ -> ())
        |> ignore

    member _.Print () =
        for computer in computers do
            computer.Print ()
