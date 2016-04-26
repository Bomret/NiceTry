namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System

module SelectExtTests = 
  [<Test>]
  let ``Trying to transform values that do not throw exceptions should result in success containing the transformed value``() = 
    (Try.Success 3).Select(fun i -> string i) |> should equal (Try.Success "3")
  
  [<Test>]
  let ``Trying to transform values that throw exceptions should result in failure``() = 
    let err = Exception "Expected err"
    (Try.Success 3).Select(fun i -> 
        raise err
        string i)
    |> should equal (Try.Failure<string> err)
  
  [<Test>]
  let ``Trying to transform failure should result in failure``() = 
    let err = Exception "Expected err"
    (Try.Failure<int> err).Select(fun i -> string i) |> should equal (Try.Failure<string> err)