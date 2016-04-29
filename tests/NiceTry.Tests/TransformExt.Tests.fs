namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module TransformExt =
    [<Test>]
    let ``Transforming a success should result in a success containing the produced result``() =
        (Try.Success 3).Transform((fun x -> x + 2), (fun _ -> -1)) |> should equal (Try.Success 5)

    [<Test>]
    let ``Transforming a failure should result in a success containing the produced result``() =
        (Try.Failure<int>(Exception())).Transform((fun x -> x + 2), (fun _ -> -1)) |> should equal (Try.Success -1)

    [<Test>]
    let ``Transforming a success and throwing an error should result in failure``() =
        let err = InvalidOperationException "Expected err"
        (Try.Success 3).Transform((fun x ->
                                  raise err
                                  x), (fun _ -> -1))
        |> should equal (Try.Failure<int> err)

    [<Test>]
    let ``Transforming a failure and throwing an error should result in failure``() =
        let err = InvalidOperationException "Expected err"
        (Try.Failure(Exception())).Transform(id,
                                             (fun _ ->
                                             raise err
                                             -1))
        |> should equal (Try.Failure<int> err)