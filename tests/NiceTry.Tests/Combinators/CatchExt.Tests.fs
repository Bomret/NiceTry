namespace NiceTry.Tests.Combinators

open System
open NUnit.Framework
open NiceTry
open NiceTry.Combinators

module CatchExtTests =
    let ``Catching and handling specific exceptions should return in success`` () =
        Assert.AreEqual(Try.Success 3, CatchExt.Catch<InvalidOperationException, int>(Try.Failure<int> (InvalidOperationException ()), fun _ -> 3))