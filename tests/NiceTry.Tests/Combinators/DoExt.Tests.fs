namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module DoExtTests = 
    [<Test>]
    let ``Trying to execute a side effect on the value of success should indeed execute it``() = 
        let original = Try.Success 3
        let mutable doResult = 0
        let result = original.Do(fun i -> doResult <- i + 1)
        Assert.AreSame(original, result)
        Assert.AreEqual(4, doResult)
    
    [<Test>]
    let ``Trying to execute a side effect on a failure should not execute it``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let mutable doResult = 0
        let result = original.Do(fun i -> doResult <- i + 1)
        Assert.AreSame(original, result)
        Assert.AreEqual(0, doResult)
    
    [<Test>]
    let ``Trying to execute a side effect that throws an error on the value of success should result in failure``() = 
        let err = InvalidOperationException()
        let original = Try.Success 3
        let result = original.Do(fun i -> raise err)
        Assert.AreEqual(Try.Failure<int> err, result)