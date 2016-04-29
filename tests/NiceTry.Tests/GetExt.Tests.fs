namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module GetExtTests = 
  [<Test>]
  let ``Getting the value of a success should result in that value``() = 
    let original = Try.Success 3
    let result = original.Get()
    result |> should equal 3
  
  [<Test>]
  let ``Getting the value of a failure should result in an InvalidOperationException``() = 
    let err = NotSupportedException()
    let original = Try.Failure<int> err
    (fun () -> original.Get() |> ignore) |> should throw typeof<InvalidOperationException>