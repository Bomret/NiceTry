namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System

module SelectExtTests = 
    [<Test>]
    let ``Trying to transform values that do not throw exceptions should always result in success containing the transformed value``() = 
        let original = Try.Success 3
        Assert.AreEqual(Try.Success 4, SelectExt.Select(original, fun i -> i + 1))
    
    [<Test>]
    let ``Trying to transform values that throw exceptions should always result in failure``() = 
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure err, SelectExt.Select(Try.Success 3, fun _ -> raise err))
    
    [<Test>]
    let ``Trying to transform failure should always result in failure``() = 
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<int> err, SelectExt.Select(Try.Failure<int> err, fun i -> i + 1))