namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module ApplyExtTests = 
    [<Test>]
    let ``Applying actions that don't throw exceptions should result in success``() = 
        let original = Try.Success 3
        let result = original.Apply(fun i -> i |> ignore)
        Assert.AreEqual(Try.Success Unit.Default, result)
    
    [<Test>]
    let ``Applying actions to failure should result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Failure<int> err
        let result = original.Apply(fun i -> i |> ignore)
        Assert.AreEqual(Try.Failure<TheVoid.Unit> err, result)
    
    [<Test>]
    let ``Applying actions that throw exceptions should result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Success 3
        let result = original.Apply(fun _ -> raise err)
        Assert.AreEqual(Try.Failure<Unit> err, result)
    
    [<Test>]
    let ``Applying functions that return Unit and don't throw exceptions should result in success``() = 
        let original = Try.Success 3
        let result = original.ApplyWith(fun _ -> Try.Success Unit.Default)
        Assert.AreEqual(Try.Success Unit.Default, result)
    
    [<Test>]
    let ``Applying functions that return Unit to failure should result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Failure<int> err
        let result = original.ApplyWith(fun _ -> Try.Success Unit.Default)
        Assert.AreEqual(Try.Failure<TheVoid.Unit> err, result)
    
    [<Test>]
    let ``Applying functions that return Unit and throw exceptions should result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Failure<int> err
        let result = original.ApplyWith(fun _ -> raise err)
        Assert.AreEqual(Try.Failure<Unit> err, result)