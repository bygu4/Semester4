module Network.Tests

open NUnit.Framework
open FsUnit

open OperatingSystem
open Computer

let performNSteps n (network: Network) =
    for _ in { 1 .. n } do
        network.ToNextIteration ()

let safeTestOs () = new OperatingSystem ("safe", 0.0)

let unsafeTestOs () = new OperatingSystem ("unsafe", 1.0)

[<Test>]
let testNetwork_EmptyNetwork_StateShouldNotChange () =
    let network = new Network ([], set [])

    performNSteps 100 network

    network.CanChange |> should be False

[<Test>]
let testNetworkWithOneComputer_Uninfected_StateShouldNotChange () =
    let computer = new Computer ("test computer", linux ())
    let network = new Network ([computer], set [])

    performNSteps 100 network

    network.CanChange |> should be False
    computer.IsInfected |> should be False
    computer.Name |> should equal "test computer"

[<Test>]
let testNetworkWithOneComputer_Infected_StateShouldNotChange () =
    let computer = new Computer ("test computer", windows (), true)
    let network = new Network ([computer], set [])

    performNSteps 100 network

    network.CanChange |> should be False
    computer.IsInfected |> should be True
    computer.Name |> should equal "test computer"

[<Test>]
let testNetworkWithTwoComputers_BothUninfected_StateShouldNotChange () =
    let computer1 = new Computer ("1", safeTestOs ())
    let computer2 = new Computer ("2", unsafeTestOs ())
    let network = new Network ([computer1; computer2], set [computer1, computer2])

    performNSteps 100 network

    network.CanChange |> should be False
    (computer1.IsInfected || computer2.IsInfected) |> should be False
    computer1.Name |> should equal "1"
    computer2.Name |> should equal "2"

[<Test>]
let testNetworkWithTwoComputers_InfectedAndSafeOS_ShouldNotBecomeInfected () =
    let computer1 = new Computer ("1", safeTestOs ())
    let computer2 = new Computer ("2", unsafeTestOs (), true)
    let network = new Network ([computer1; computer2], set [computer1, computer2])

    performNSteps 100 network

    network.CanChange |> should be False
    computer1.IsInfected |> should be False
    computer2.IsInfected |> should be True

[<Test>]
let testNetworkWithTwoComputers_InfectedAndUnsafeOS_ShouldBecomeInfected () =
    let computer1 = new Computer ("1", safeTestOs (), true)
    let computer2 = new Computer ("2", unsafeTestOs ())
    let network = new Network ([computer1; computer2], set [computer1, computer2])

    network.ToNextIteration ()

    (computer1.IsInfected && computer2.IsInfected) |> should be True

[<Test>]
let testNetworkWithComputersInLine_UnsafeOsAndFirstInfected_ShouldBecomeInfectedOneByOne () =
    let n = 12
    let computers =
        { 1 .. n }
        |> Seq.map (fun i -> new Computer (i.ToString (), unsafeTestOs ()))
        |> Seq.toArray
    computers[0].Infect ()
    let links =
        { 1 .. (n - 1) }
        |> Seq.map (fun i -> computers[i - 1], computers[i])
        |> set
    let network = new Network (computers, links)

    network.ToNextIteration ()
    computers[1].IsInfected |> should be True
    computers[2].IsInfected |> should be False
    network.CanChange |> should be True

    network.ToNextIteration ()
    computers[2].IsInfected |> should be True
    computers[3].IsInfected |> should be False
    network.CanChange |> should be True

    performNSteps n network
    computers |> Seq.map _.IsInfected |> Seq.exists (( = ) false) |> should be False
    network.CanChange |> should be False

[<Test>]
let testNetwork_ComplexNetwork_UnsafeOSComputersBecomeInfected () =
    let computers = [
        new Computer ("1", unsafeTestOs (), true);
        new Computer ("2", safeTestOs ());
        new Computer ("3", unsafeTestOs ());
        new Computer ("4", unsafeTestOs ());
        new Computer ("5", safeTestOs (), true);
        new Computer ("6", unsafeTestOs ());
        new Computer ("7", safeTestOs ());
        new Computer ("8", unsafeTestOs ());
    ]

    let links = set [
        computers[0], computers[1];
        computers[0], computers[2];
        computers[1], computers[2];
        computers[2], computers[3];
        computers[3], computers[4];
        computers[3], computers[5];
        computers[2], computers[6];
        computers[6], computers[7];
    ]

    let network = new Network (computers, links)

    network.ToNextIteration ()
    network.CanChange |> should be True
    computers |> List.map _.IsInfected |> should equal [true; false; true; true; true; false; false; false]

    performNSteps 100 network
    network.CanChange |> should be False
    computers |> List.map _.IsInfected |> should equal [true; false; true; true; true; true; false; false]
