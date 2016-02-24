namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module FlattenExtTests = 
  [<Test>]
  let ``Flattening a nested success should result in the inner Try<T>``() = 
    let original = Try.Success(Try.Success 3)
    original.Flatten() |> should equal (Try.Success 3)
  
  [<Test>]
  let ``Flattening a nested failure should result in failure``() = 
    let err = InvalidOperationException()
    let original = Try.Failure<Try<int>> err
    original.Flatten() |> should equal (Try.Failure<int> err)