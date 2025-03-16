module Network

open System
open Computer

type Link = Computer * Computer
type Links = Link list

let shouldBecomeInfected (chance : InfectionChance): IsInfected =
    let random = new Random (DateTime.Now.Ticks % int64 Int32.MaxValue |> int)
    random.NextDouble () < chance

type Network (computers: Computers, links: Links) =
    member _.ToNextIteration () =
        let rec getNewInfected (links: Links) (acc: Computers): Computers =
            match links with
            | (comp1, comp2) :: tail when comp1.IsInfected && not comp2.IsInfected ->
                let infectionChance = infectionChance comp2.OS
                if shouldBecomeInfected infectionChance then
                    getNewInfected tail (comp2 :: acc)
                else getNewInfected tail acc
            | (comp1, comp2) :: tail when not comp1.IsInfected && comp2.IsInfected ->
                let infectionChance = infectionChance comp1.OS
                if shouldBecomeInfected infectionChance then
                    getNewInfected tail (comp1 :: acc)
                else getNewInfected tail acc
            | _ :: tail -> getNewInfected tail acc
            | [] -> acc

        let toInfect = getNewInfected links []
        for comp in toInfect do
            comp.Infect ()

    member _.Print () =
        for computer in computers do
            computer.Print ()
