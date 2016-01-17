namespace NiceTry.Tests

open System
open NUnit.Framework
open FsCheck.NUnit
open NiceTry
open TheVoid

module TryCreationTests =
    [<Test>]
    let ``Trying to execute actions that do not throw exceptions should always result in success`` () =
        Assert.AreEqual(Try.To (fun () -> ()), Try.Success Unit.Default)

    [<Property>]
    let ``Trying to execute functions that do not throw exceptions should always result in success`` () =
        let func = fun () -> "test"
        Assert.AreEqual(Try.To func, Try.Success (func ()))

    [<Test>]
    let ``Trying to execute functions that do not throw exceptions and return instances of ITry<T> should always result in these instances`` () =
        let func = fun () -> Try.Success "test"
        
        Assert.AreEqual(Try.To<string> func, func ())

    [<Test>]
    let ``Trying to execute actions that throw exceptions should always result in failure`` () =
        let err = Exception "Expected err"
        let throw = fun () -> raise err; ()

        Assert.AreEqual(Try.To throw, Try.Failure<Unit> err)

    [<Test>]
    let ``Trying to execute functions that throw exceptions should always result in failure`` () =
        let err = Exception "Expected err"
        let throw = fun () ->
            raise err
            0

        Assert.AreEqual(Try.To throw, Try.Failure<int> err)