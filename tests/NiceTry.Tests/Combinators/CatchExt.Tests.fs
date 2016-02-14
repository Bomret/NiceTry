namespace NiceTry.Tests.Combinators

open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module CatchExtTests = 
    [<Test>]
    let ``Catching and handling specific exceptions should result in success``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let result = original.Catch<InvalidOperationException, int>(fun _ -> 3)
        Assert.AreEqual(Try.Success 3, result)
    
    [<Test>]
    let ``Not catching and handling specific exceptions should result in failure``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let result = original.Catch<ArgumentException, int>(fun _ -> 3)
        Assert.AreEqual(Try.Failure<int> err, result)
    
    [<Test>]
    let ``Catching and failing to handle specific exceptions should result in failure``() = 
        let err = InvalidOperationException()
        let newErr = NotSupportedException()
        let original = Try.Failure<int> err
        let result = original.Catch<InvalidOperationException, int>(fun _ -> raise newErr)
        Assert.AreEqual(Try.Failure<int> newErr, result)
    
    [<Test>]
    let ``Catching and handling specific exceptions on success should result in success and not trigger the error handler``() = 
        let original = Try.Success 3
        let mutable wasTriggered = false
        
        let result = 
            original.Catch<InvalidOperationException, int>(fun _ -> 
                wasTriggered <- true
                0)
        Assert.IsFalse wasTriggered
        Assert.AreEqual(Try.Success 3, result)
    
    [<Test>]
    let ``Catching and handling specific exceptions with functions that return Try<T> should result in those Try<T>``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let result = original.CatchWith<InvalidOperationException, int>(fun _ -> Try.Success 3)
        Assert.AreEqual(Try.Success 3, result)
    
    [<Test>]
    let ``Not catching and handling specific exceptions with functions that return Try<T> should result in failure``() = 
        let err = InvalidOperationException()
        let original = Try.Failure<int> err
        let result = original.CatchWith<ArgumentException, int>(fun _ -> Try.Success 3)
        Assert.AreEqual(Try.Failure<int> err, result)
    
    [<Test>]
    let ``Catching and failing to handle specific exceptions with functions that return Try<T> should result in failure``() = 
        let err = InvalidOperationException()
        let newErr = NotSupportedException()
        let original = Try.Failure<int> err
        
        let result = 
            original.CatchWith<InvalidOperationException, int>(fun _ -> 
                Try.To(fun () -> 
                    raise newErr
                    3))
        Assert.AreEqual(Try.Failure<int> newErr, result)
    
    [<Test>]
    let ``Catching and handling specific exceptions with functions that return Try<T> on success should result in success and not trigger the error handler``() = 
        let original = Try.Success 3
        let mutable wasTriggered = false
        
        let result = 
            original.CatchWith<InvalidOperationException, int>(fun _ -> 
                wasTriggered <- true
                Try.Success 0)
        Assert.IsFalse wasTriggered
        Assert.AreEqual(Try.Success 3, result)