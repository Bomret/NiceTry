namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module ApplyExtTests = 
    [<Test>]
    let ``Applying actions that don't throw exceptions should result in success``() = 
        (Try.Success 3).Apply(fun _ -> ignore()) |> should equal (Try.Success Unit.Default)
    
    [<Test>]
    let ``Applying actions to failure should result in failure``() = 
        let err = Exception "Expected error"
        (Try.Failure<int> err).Apply(fun i -> ignore()) |> should equal (Try.Failure<Unit> err)
    
    [<Test>]
    let ``Applying actions that throw exceptions should result in failure``() = 
        let err = Exception "Expected error"
        (Try.Success 3).Apply(fun _ -> raise err) |> should equal (Try.Failure<Unit> err)
    
    [<Test>]
    let ``Applying functions that return Unit and don't throw exceptions should result in success``() = 
        (Try.Success 3).ApplyWith(fun _ -> Try.Success Unit.Default) |> should equal (Try.Success Unit.Default)
    
    [<Test>]
    let ``Applying functions that return Unit to failure should result in failure``() = 
        let err = Exception "Expected error"
        (Try.Failure<int> err).ApplyWith(fun _ -> Try.Success Unit.Default) |> should equal (Try.Failure<Unit> err)
    
    [<Test>]
    let ``Applying functions that return Unit and throw exceptions should result in failure``() = 
        let err = Exception "Expected error"
        (Try.Failure<int> err).ApplyWith(fun _ -> raise err) |> should equal (Try.Failure<Unit> err)