namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module SingleTryExt =
    [<Test>]
    let ``Getting the single element of an enumerable should result in success``() =
        let enumerable = [| 1 |]
        enumerable.SingleTry() |> should equal (Try.Success 1)

    [<Test>]
    let ``Getting the single matching element of an enumerable should result in success``() =
        let enumerable = [| 1; 2; 3 |]
        enumerable.SingleTry(fun x -> x = 2) |> should equal (Try.Success 2)

    [<Test>]
    let ``Getting a single element of an enumerable that contains several elements should result in failure``() =
        let enumerable = [| 1; 2; 3 |]
        enumerable.SingleTry() |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Getting a single element of an enumerable that is empty should result in failure``() =
        let enumerable = Seq.empty<int>
        enumerable.SingleTry() |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Getting the single matching element of an enumerable that contains no matching element should result in failure``() =
        let enumerable = [| 1; 2; 3 |]
        enumerable.SingleTry(fun x -> x = 4) |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Getting the single matching element of an enumerable that contains several matching elements should result in failure``() =
        let enumerable = [| 1; 3; 3 |]
        enumerable.SingleTry(fun x -> x = 3) |> should be instanceOfType<Failure<int>>