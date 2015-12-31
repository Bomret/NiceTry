namespace NiceTry.Tests

open System
open FsCheck.NUnit
open NiceTry

module TryCreationTests =
    [<Property>]
    let ``Trying to execute actions that do not throw exceptions should always result in success``(action : unit -> unit) =
        Try.To(action) = Try.Success()

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions should always result in success``(func : unit -> string) =
        Try.To(func) = (func() |> Try.Success)

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions and return instances of ITry should always result in these instances``() =
        let func : unit -> ITry = fun () -> Try.Success()
        
        Try.To(func) = func()

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions and return instances of ITry<T> should always result in these instances``() =
        let func : unit -> ITry<string> = fun () -> Try.Success "test"
        
        Try.To<string>(func) = func()

    [<Property>]
    let ``Trying to execute actions that throw exceptions should always result in failure``() =
        let err = Exception "bla"
        let throw = fun () -> (raise err) |> ignore

        Try.To(throw) = Try.Failure(err)

    [<Property>]
    let ``Trying to execute functions that throw exceptions should always result in failure``() =
        let err = Exception "bla"
        let throw = fun () ->
            raise err
            0

        Try.To(throw) = Try.Failure<int>(err)