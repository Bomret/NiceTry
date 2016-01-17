module NiceTry.Tests.Combinators

open System
open FsCheck.NUnit
open NiceTry
open NiceTry.Combinators

module SelectExtTests =
    [<Property>]
    let ``Trying to transform values that do not throw exceptions should always result in success containing the transformed value`` () =
        Try.Success 4 = SelectExt.Select(Try.Success 3, fun i -> i + 1)

    [<Property>]
    let ``Trying to transform values that throw exceptions should always result in failure`` () =
        let throw = fun () -> failwith "Expected err"
        Try.Failure <| Exception "Expected err" = SelectExt.Select(Try.Success 3, fun _ -> throw ())

    [<Property>]
    let ``Trying to transform failure should always result in failure`` () =
        let err = Exception "Expected err"
        Try.Failure<int> err = SelectExt.Select(Try.Failure<int> err, fun i -> i + 1)