namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module CatchExtTests =
  [<Test>]
  let ``Catching and handling specific exceptions should result in success``() =
    let err = InvalidOperationException "Expected error"
    (Try.Failure<int> err).Catch<InvalidOperationException, int> (fun _ -> 3) |> should equal (Try.Success 3)

  [<Test>]
  let ``Not catching and handling specific exceptions should result in failure``() =
    let err = InvalidOperationException "Expected error"
    (Try.Failure<int> err).Catch<ArgumentException, int> (fun _ -> 3) |> should equal (Try.Failure<int> err)

  [<Test>]
  let ``Catching and failing to handle specific exceptions should result in failure``() =
    let err = InvalidOperationException "Expected error"
    let newErr = NotSupportedException "Expected followup error"
    (Try.Failure<int> err).Catch<InvalidOperationException, int> (fun _ -> raise newErr)
    |> should equal (Try.Failure<int> newErr)

  [<Test>]
  let ``Catching and handling specific exceptions on success should result in success and not trigger the error handler``() =
    let original = Try.Success 3
    let mutable wasTriggered = false

    let result =
        original.Catch<InvalidOperationException, int>(fun _ ->
            wasTriggered <- true
            0)
    result |> should equal (Try.Success 3)
    wasTriggered |> should be False

  [<Test>]
  let ``Catching and handling specific exceptions with functions that return Try<T> should result in those Try<T>``() =
    let err = InvalidOperationException "Expected error"
    (Try.Failure<int> err).CatchWith<InvalidOperationException, int> (fun _ -> Try.Success 3)
    |> should equal (Try.Success 3)

  [<Test>]
  let ``Not catching and handling specific exceptions with functions that return Try<T> should result in failure``() =
    let err = InvalidOperationException "Expected error"
    (Try.Failure<int> err).CatchWith<ArgumentException, int> (fun _ -> Try.Success 3)
    |> should equal (Try.Failure<int> err)

  [<Test>]
  let ``Catching and failing to handle specific exceptions with functions that return Try<T> should result in failure``() =
    let err = InvalidOperationException "Expected error"
    let newErr = NotSupportedException "Expected followup error"
    let original = Try.Failure<int> err

    let result =
        original.CatchWith<InvalidOperationException, int>(fun _ ->
            Try.To(fun () ->
                raise newErr
                3))
    result |> should equal (Try.Failure<int> newErr)

  [<Test>]
  let ``Catching and handling specific exceptions with functions that return Try<T> on success should result in success and not trigger the error handler``() =
    let original = Try.Success 3
    let mutable wasTriggered = false

    let result =
        original.CatchWith<InvalidOperationException, int>(fun _ ->
            wasTriggered <- true
            Try.Success 0)
    result |> should equal (Try.Success 3)
    wasTriggered |> should be False