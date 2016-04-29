namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module SelectValuesExt =
    [<Test>]
    let ```Selecting the values from an enumerable that contains successes and failures should result in the successful values``() =
        let enumerable =
            [ Try.Success(1)
              Try.Failure<int>(Exception())
              Try.Success(3) ]
        enumerable.SelectValues() |> should equal [ 1; 3 ]

    [<Test>]
    let ```Selecting the values from an enumerable that contains only successes should result in all values``() =
        let enumerable =
            [ Try.Success(1)
              Try.Success(2)
              Try.Success(3) ]
        enumerable.SelectValues() |> should equal [ 1; 2; 3 ]

    [<Test>]
    let ```Selecting the values from an enumerable that contains only failures should result in an empty enumerable``() =
        let enumerable =
            [ Try.Failure<int>(Exception())
              Try.Failure<int>(Exception())
              Try.Failure<int>(Exception()) ]
        enumerable.SelectValues() |> should be Empty