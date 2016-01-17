module NiceTry.Tests.Combinators

open System
open NUnit.Framework
open FsCheck.NUnit
open NiceTry
open NiceTry.Combinators

module SelectExtTests =
    [<Test>]
    let ``Trying to transform values that do not throw exceptions should always result in success containing the transformed value`` () =
        Assert.AreEqual(Try.Success 4, SelectExt.Select(Try.Success 3, fun i -> i + 1))

    [<Test>]
    let ``Trying to transform values that throw exceptions should always result in failure`` () =
        let err = Exception "Exception"
        let throw = fun () -> raise err
        Assert.AreEqual(Try.Failure err, SelectExt.Select(Try.Success 3, fun _ -> throw ()))

    [<Test>]
    let ``Trying to transform failure should always result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<int> err, SelectExt.Select(Try.Failure<int> err, fun i -> i + 1))