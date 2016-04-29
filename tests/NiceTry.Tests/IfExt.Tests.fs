namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module IfExtTests =
    [<Test>]
    let ``Executing a side effect on a success on the condition that it is a success should execute the side effect``() =
        let mutable wasSideEffectExecuted = false
        (Try.Success 3).IfSuccess(fun _ -> wasSideEffectExecuted <- true)
        wasSideEffectExecuted |> should be True

    [<Test>]
    let ``Executing a side effect on a failure on the condition that it is a success should not execute the side effect``() =
        let err = Exception "Expected error"
        let mutable wasSideEffectExecuted = false
        (Try.Failure<int> err).IfSuccess(fun _ -> wasSideEffectExecuted <- true)
        wasSideEffectExecuted |> should be False