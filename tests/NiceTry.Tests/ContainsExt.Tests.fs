namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module ContainsExt =
    [<Test>]
    let ``Checking if a success contains a desired value which contains it should return true``() =
        (Try.Success 3).Contains(3) |> should be True

    [<Test>]
    let ``Checking if a success contains a desired value which does not contain it should return false``() =
        (Try.Success 3).Contains(4) |> should be False

    [<Test>]
    let ``Checking if a failure contains a desired value should return false``() =
        (Try.Failure<int>(InvalidOperationException())).Contains(3) |> should be False

    [<Test>]
    let ``Checking if a success contains a desired value which contains it using a compare function should return true``() =
        (Try.Success 3).Contains(3, Func<_, _, _> (=)) |> should be True

    [<Test>]
    let ``Checking if a success contains a desired value which does not contain it using a compare function should return false``() =
        (Try.Success 3).Contains(4, Func<_, _, _> (=)) |> should be False

    [<Test>]
    let ``Checking if a failure contains a desired value using a compare function should return false``() =
        (Try.Failure<int>(InvalidOperationException())).Contains(3, Func<_, _, _> (=)) |> should be False