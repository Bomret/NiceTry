namespace NiceTry.Tests.Combinators

open System
open NUnit.Framework
open NiceTry
open NiceTry.Combinators

module SelectManyExtTests =
    [<Test>]
    let ``Trying to transform values that do not throw exceptions should always result in the resulting ITry`` () =
        Assert.AreEqual(Try.Success 4, SelectManyExt.SelectMany(Try.Success 3, fun i -> Try.To (fun () -> i + 1)))

    [<Test>]
    let ``Trying to transform values that throw exceptions should always result in failure`` () =
        let err = Exception "Expected err"
        let throw = fun () -> raise err; 4
        Assert.AreEqual(Try.Failure<int> err, SelectManyExt.SelectMany(Try.Success 3, fun _ -> Try.To (fun () -> throw ())))

    [<Test>]
    let ``Trying to transform failure should always result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<int> err, SelectManyExt.SelectMany(Try.Failure<int> err, fun i -> Try.To (fun () -> i + 1)))
