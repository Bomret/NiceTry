namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System

module SelectManyExtTests = 
    [<Test>]
    let ``Trying to transform values that do not throw exceptions should always result in the resulting Try``() = 
        (Try.Success 3).SelectMany(fun i -> Try.To(fun () -> string i)) |> should equal (Try.Success "3")
    
    [<Test>]
    let ``Trying to transform values that throw exceptions should always result in failure``() = 
        let err = NotSupportedException()
        (Try.Success 3).SelectMany(fun i -> 
            Try.To(fun () -> 
                raise err
                string i))
        |> should equal (Try.Failure<string> err)
    
    [<Test>]
    let ``Trying to transform failure should always result in failure``() = 
        let err = Exception "Expected err"
        (Try.Failure<int> err).SelectMany(fun i -> Try.To(fun () -> string i)) |> should equal (Try.Failure<string> err)
