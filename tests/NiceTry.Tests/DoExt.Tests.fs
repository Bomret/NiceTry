namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module DoExtTests =
  [<Test>]
  let ``Trying to execute a side effect on the value of success should indeed execute it``() =
    let original = Try.Success 3
    let mutable doResult = 0
    original.Do(fun i -> doResult <- i + 1) |> should be (sameAs original)
    doResult |> should equal 4

  [<Test>]
  let ``Trying to execute a side effect on a failure should not execute it``() =
    let err = InvalidOperationException()
    let original = Try.Failure<int> err
    let mutable doResult = 0
    original.Do(fun i -> doResult <- i + 1) |> should be (sameAs original)
    doResult |> should equal 0

  [<Test>]
  let ``Trying to execute a side effect that throws an error on the value of success should result in failure``() =
    let err = InvalidOperationException()
    (Try.Success 3).Do(fun i -> raise err) |> should equal (Try.Failure<int> err)