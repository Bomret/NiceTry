namespace NiceTry.Tests.Combinators

open FsUnit
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
    original.Finally(fun () -> wasSideEffectExecuted <- true) |> should be (sameAs original)
    wasSideEffectExecuted |> should be True
  
  [<Test>]
  let ``Executing a finally side effect on a failure should indeed execute it``() = 
    let err = InvalidOperationException "Expected error"
    let original = Try.Failure<int> err
    let mutable wasSideEffectExecuted = false
    original.Finally(fun () -> wasSideEffectExecuted <- true) |> should be (sameAs original)
    wasSideEffectExecuted |> should be True
  
  [<Test>]
  let ``Executing a failing finally side effect on a success should result in failure``() = 
    let err = InvalidOperationException "Expected error"
    (Try.Success 3).Finally(fun () -> raise err) |> should equal (Try.Failure<int> err)
  
  [<Test>]
  let ``Executing a failing finally side effect on a failure should result in failure``() = 
    let err = InvalidOperationException "Expected error"
    let newErr = NotSupportedException "Expected followup error"
    (Try.Failure<int> err).Finally(fun () -> raise newErr) |> should equal (Try.Failure<int> newErr)