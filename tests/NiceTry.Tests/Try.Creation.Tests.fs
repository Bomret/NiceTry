namespace NiceTry.Tests

open FsUnit
open NUnit.Framework
open NiceTry
open System
open TheVoid

module TryCreationTests = 
    [<Test>]
    let ``Trying to execute actions that do not throw exceptions should always result in success``() = 
        Try.To(fun () -> ()) |> should equal (Try.Success Unit.Default)
    
    [<Test>]
    let ``Trying to execute functions that do not throw exceptions should always result in success``() = 
        let func = fun () -> "test"
        Try.To func |> should equal (Try.Success(func()))
    
    [<Test>]
    let ``Trying to execute functions that do not throw exceptions and return instances of Try<T> should always result in these instances``() = 
        let func = fun () -> Try.Success "test"
        Try.To<string> func |> should equal (func())
    
    [<Test>]
    let ``Trying to execute actions that throw exceptions should always result in failure``() = 
        let err = Exception "Expected err"
        Try.To(fun () -> 
            raise err
            Unit.Default)
        |> should equal (Try.Failure<Unit> err)
    
    [<Test>]
    let ``Trying to execute functions that throw exceptions should always result in failure``() = 
        let err = Exception "Expected err"
        Try.To(fun () -> 
            raise err
            0)
        |> should equal (Try.Failure<int> err)