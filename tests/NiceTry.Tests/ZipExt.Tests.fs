namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module ZipExt =
    [<Test>]
    let ``Zipping two successes should result in success``() =
        let one = Try.Success 1
        let two = Try.Success 2
        one.Zip(two, Func<_, _, _> (+)) |> should equal (Try.Success 3)

    [<Test>]
    let ``Zipping a success and a failure should result in failure``() =
        let one = Try.Success 1
        let two = Try.Failure<int>(Exception())
        one.Zip(two, Func<_, _, _> (+)) |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Zipping two successes but throwing an exception should result in failure``() =
        let one = Try.Success 1
        let two = Try.Success 2
        let err = InvalidOperationException "Expected err"

        let zip =
            fun x y ->
                raise err
                0
        one.Zip(two, Func<_, _, _> zip) |> should equal (Try.Failure<int> err)