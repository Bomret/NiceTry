namespace NiceTry.Tests

open System
open FsCheck.NUnit
open NiceTry
open TheVoid

module TryCreationTests =
    [<Property>]
    let ``Trying to execute actions that do not throw exceptions should always result in success`` (action : unit -> unit) =
        Try.To action  = Try.Success Unit.Default

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions should always result in success`` (func : unit -> string) =
        Try.To func = Try.Success (func ())

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions and return instances of ITry<T> should always result in these instances`` () =
        let func = fun () -> Try.Success "test"
        
        Try.To<string> func = func ()

    [<Property>]
    let ``Trying to execute actions that throw exceptions should always result in failure`` () =
        let err = Exception "Expected err"
        let throw = fun () -> raise err; ()

        Try.To throw = Try.Failure<Unit> err

    [<Property>]
    let ``Trying to execute functions that throw exceptions should always result in failure`` () =
        let err = Exception "Expected err"
        let throw = fun () ->
            raise err
            0

        Try.To throw = Try.Failure<int> err