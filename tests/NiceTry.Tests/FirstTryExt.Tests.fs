namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module FirstTryExt =
    [<Test>]
    let ```Getting the first element from an enumerable that contains exactly one should return a success``() =
        let enumerable = [ 1 ] |> Seq.ofList
        enumerable.FirstTry() |> should equal (Try.Success 1)

    [<Test>]
    let ```Getting the first element from an enumerable that contains several ones should return a success``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.FirstTry() |> should equal (Try.Success 1)

    [<Test>]
    let ```Getting the first matching element from an enumerable that contains several ones should return a success``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.FirstTry(fun x -> x = 3) |> should equal (Try.Success 3)

    [<Test>]
    let ```Getting the first matching element from an enumerable that contains no matching element should return a failure``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.FirstTry(fun x -> x = 4) |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ```Getting the first matching element from an enumerable that contains several matching element should return a success``() =
        let enumerable = [ 3; 2; 3 ] |> Seq.ofList
        enumerable.FirstTry(fun x -> x = 3) |> should equal (Try.Success 3)

    [<Test>]
    let ```Getting the first element from an enumerable that is empty should return a failure``() =
        let enumerable = Seq.empty<int>
        enumerable.FirstTry() |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ```Getting the first matching element from an enumerable that is empty should return a failure``() =
        let enumerable = Seq.empty<int>
        enumerable.FirstTry(fun x -> x = 3) |> should be instanceOfType<Failure<int>>