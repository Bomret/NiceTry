namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open System.Collections.Generic
open TheVoid

module ExchangeExt =
    [<Test>]
    let ``Exchanging a success of an enumerable containing elements should return an enumerable of successes``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        (Try.Success enumerable).Exchange() |> should equal (enumerable |> Seq.map Try.Success)

    [<Test>]
    let ``Exchanging a failure should return an enumerable containing this failure``() =
        let err = InvalidOperationException "Expected err"
        let failure = Try.Failure<IEnumerable<int>> err
        failure.Exchange() |> should equal [| Try.Failure<int>(err) |]