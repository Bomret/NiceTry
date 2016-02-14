namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System

module SelectManyExtTests = 
    [<Test>]
    let ``Trying to transform values that do not throw exceptions should always result in the resulting Try``() = 
        let original = Try.Success 3
        let result = original.SelectMany(fun i -> Try.To(fun () -> i + 1))
        Assert.AreEqual(Try.Success 4, result)
    
    [<Test>]
    let ``Trying to transform values that throw exceptions should always result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Success 3
        
        let result = 
            original.SelectMany(fun _ -> 
                Try.To(fun () -> 
                    raise err
                    4))
        Assert.AreEqual(Try.Failure<int> err, result)
    
    [<Test>]
    let ``Trying to transform failure should always result in failure``() = 
        let err = Exception "Expected err"
        let original = Try.Failure<int> err
        let result = original.SelectMany(fun i -> Try.To(fun () -> i + 1))
        Assert.AreEqual(Try.Failure<int> err, result)
