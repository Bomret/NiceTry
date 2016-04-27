namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module OrElseExtTests =
    [<Test>]
    let ``Providing a fallback for a success should result in the success``() =
        let original = Try.Success 3
        let fallback = -1
        original.OrElse(fallback) |> should be (sameAs original)

    [<Test>]
    let ``Providing a fallback for a failure should result in the fallback``() =
        let original = Try.Failure<int>(Exception "Unexpected error")
        original.OrElse(3) |> should equal (Try.Success 3)

    [<Test>]
    let ``Providing a fallback Try<T> for a success should result in the success``() =
        let original = Try.Success 3
        let fallback = Try.Success -1
        original.OrElse(fallback) |> should be (sameAs original)

    [<Test>]
    let ``Providing a fallback Try<T> for a failure should result in the fallback``() =
        let original = Try.Failure<int>(Exception "Unexpected error")
        let fallback = Try.Success 3
        original.OrElse(fallback) |> should equal fallback