namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module FinallyExtTests = 
    [<Test>]
    let ``Executing a finally side effect on a success should indeed execute it``() = 
        let original = Try.Success 3
        let mutable wasSideEffectExecuted = false
        let result = original.Finally(fun () -> wasSideEffectExecuted <- true)
        Assert.AreSame(original, result)
        Assert.IsTrue wasSideEffectExecuted
    
    [<Test>]
    let ``Executing a finally side effect on a failure should indeed execute it``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let mutable wasSideEffectExecuted = false
        let result = original.Finally(fun () -> wasSideEffectExecuted <- true)
        Assert.AreSame(original, result)
        Assert.IsTrue wasSideEffectExecuted
    
    [<Test>]
    let ``Executing a failing finally side effect on a success should result in failure``() = 
        let err = InvalidOperationException()
        let original = Try.Success 3
        let result = original.Finally(fun () -> raise err)
        Assert.AreEqual(Try.Failure<int> err, result)
    
    [<Test>]
    let ``Executing a failing finally side effect on a failure should result in failure``() = 
        let err = InvalidOperationException()
        let newErr = NotSupportedException()
        let original = Try.Failure<int> err
        let result = original.Finally(fun () -> raise newErr)
        Assert.AreEqual(Try.Failure<int> newErr, result)