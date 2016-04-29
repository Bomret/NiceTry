namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open System.Collections.Generic
open System.Linq
open TheVoid

module AllOrFailureExt =
    [<Test>]
    let ``Calling AllOrFailure on an enumerable that contains only success should result in all elements``() =
        let expected = [ 1; 2; 3 ] |> Seq.ofList
        let enumerable = expected |> Seq.map Try.Success
        enumerable.AllOrFailure().Get() |> should equal expected

    [<Test>]
    let ``Calling AllOrFailure on an enumerable that also contains failure should result in failure``() =
        let err = InvalidOperationException "Expected error"

        let enumerable =
            [ Try.Success<int>(1)
              Try.Failure<int>(err)
              Try.Success<int>(2) ]
            |> Seq.ofList
        enumerable.AllOrFailure() |> should equal (Try.Failure<IEnumerable<int>> err)