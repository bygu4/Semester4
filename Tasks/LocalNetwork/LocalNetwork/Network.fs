module Network

open Computer

type Computers = Computer Set

type Link = Computer * Computer
type Links = Link list

type Network (computers: Computers, links: Links) =
    let mutable canChange = true

    member _.CanChange = canChange

    member _.ToNextIteration () =
        let rec getNewInfected (links: Links) (acc: Computer list): Computer list =
            match links with
            | (comp1, comp2) :: tail when comp1.IsInfected && not comp2.IsInfected ->
                if comp2.CanBecomeInfected then canChange <- true
                if comp2.ShouldBecomeInfected () then
                    getNewInfected tail (comp2 :: acc)
                else getNewInfected tail acc
            | (comp1, comp2) :: tail when not comp1.IsInfected && comp2.IsInfected ->
                if comp1.CanBecomeInfected then canChange <- true
                if comp1.ShouldBecomeInfected () then
                    getNewInfected tail (comp1 :: acc)
                else getNewInfected tail acc
            | _ :: tail -> getNewInfected tail acc
            | [] -> acc

        canChange <- false
        let toInfect = getNewInfected links []
        for comp in toInfect do
            comp.Infect ()

    member _.Print () =
        for computer in computers do
            computer.Print ()
