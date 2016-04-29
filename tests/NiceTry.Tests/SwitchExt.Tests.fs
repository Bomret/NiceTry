namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module SwitchExt =
    open System.Collections.Generic

    [<Test>]
    let ``Switching on a success or an enumerable of Try<T> should return the first success``() =
        let enumerable =
            [| Try.Success(1)
               Try.Failure<int>(Exception())
               Try.Success(3) |]
        (Try.Success(0)).Switch(enumerable) |> should equal (Try.Success 0)

    [<Test>]
    let ``Switching on a failure or an enumerable of Try<T> should return the first success``() =
        let enumerable =
            [| Try.Success(1)
               Try.Failure<int>(Exception())
               Try.Success(3) |]
        (Try.Failure<int>(Exception())).Switch(enumerable) |> should equal (Try.Success 1)

    [<Test>]
    let ``Switching on a success or an empty enumerable should return the success``() =
        let enumerable = Seq.empty<Try<int>>
        (Try.Success(0)).Switch(enumerable) |> should equal (Try.Success 0)

    [<Test>]
    let ``Switching on a failure or an empty enumerable should result in failure``() =
        let enumerable = Seq.empty<Try<int>>
        (Try.Failure<int>(Exception())).Switch(enumerable) |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Switching on an enumerable of successes only should return the first success``() =
        let enumerable =
            [| Try.Success(1)
               Try.Success(2)
               Try.Success(3) |]
        enumerable.Switch() |> should equal (Try.Success 1)

    [<Test>]
    let ``Switching on an enumerable of failures only should result in failure``() =
        let enumerable =
            [| Try.Failure<int>(Exception())
               Try.Failure<int>(Exception())
               Try.Failure<int>(Exception()) |]
        enumerable.Switch() |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Switching on an empty enumerable should result in failure``() =
        let enumerable = Seq.empty<Try<int>>
        enumerable.Switch() |> should be instanceOfType<Failure<int>>

    [<Test>]
    let ``Switching on an enumerable of successes and failures should return the first success``() =
        let enumerable =
            [| Try.Failure<int>(Exception())
               Try.Failure<int>(Exception())
               Try.Success(2) |]
        enumerable.Switch() |> should equal (Try.Success 2)