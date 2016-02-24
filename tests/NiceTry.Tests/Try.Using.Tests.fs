namespace NiceTry.Tests

open FsUnit
open NUnit.Framework
open NiceTry
open System
open TheVoid

module TryUsingTests = 
  type TestDisposable() = 
    member val IsDisposed = false with get, set
    interface IDisposable with
      member x.Dispose() = x.IsDisposed <- true
  
  [<Test>]
  let ``Trying and succeeding using a IDisposable should dispose it properly``() = 
    let disp = new TestDisposable()
    let createD = fun () -> disp
    let useD = fun d -> 42
    Try.Using(createD, useD) |> should equal (Try.Success 42)
    disp.IsDisposed |> should be True
  
  [<Test>]
  let ``Trying and failing using a IDisposable should dispose it properly``() = 
    let err = Exception "Expected err"
    let disp = new TestDisposable()
    let createD = fun () -> disp
    
    let useD = fun d -> raise err; 42
    Try.Using(createD, useD) |> should equal (Try.Failure<int> err)
    disp.IsDisposed |> should be True
