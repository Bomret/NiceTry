namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module RecoverExtTests =
  [<Test>]
  let ``Recovering from a success should result in this success`` () =
    let original = Try.Success 3
    let mutable wasExecuted = false

    original.Recover(fun err -> wasExecuted <- true; -1) |> should be (sameAs original)
    wasExecuted |> should be False

  [<Test>]
  let ``Recovering from a failure should result in a success containing the recovered value`` () =
    let original = Try.Failure<int> (Exception "Unexpected error")
    let mutable wasExecuted = false

    original.Recover(fun err -> wasExecuted <- true; -1) |> should equal (Try.Success -1)
    wasExecuted |> should be True

  [<Test>]
  let ``Failing to recover from a failure should result in failure`` () =
    let original = Try.Failure<int> (Exception "Unexpected error")
    let newErr = Exception "Expected err"

    original.Recover(fun err -> raise newErr; -1) |> should equal (Try.Failure<int> newErr)

  [<Test>]
  let ``Recovering from a success with a success should result in the original success`` () =
    let original = Try.Success 3
    let mutable wasExecuted = false

    original.RecoverWith(fun err -> wasExecuted <- true; Try.Success -1) |> should be (sameAs original)
    wasExecuted |> should be False

  [<Test>]
  let ``Recovering from a failure with a success should result in the success`` () =
    let original = Try.Failure<int> (Exception "Unexpected error")
    let mutable wasExecuted = false

    original.RecoverWith(fun err -> wasExecuted <- true; Try.Success -1) |> should equal (Try.Success -1)
    wasExecuted |> should be True

  [<Test>]
  let ``Failing to recover from a failure with a success should result in failure`` () =
    let original = Try.Failure<int> (Exception "Unexpected error")
    let newErr = Exception "Expected err"

    original.RecoverWith(fun err -> raise newErr; Try.Success -1) |> should equal (Try.Failure<int> newErr)