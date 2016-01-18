namespace NiceTry.Tests.Combinators

open System
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open TheVoid

module ApplyExtTests =
    [<Test>]
    let ``Applying actions that don't throw exceptions should result in success`` () =
        Assert.AreEqual(Try.Success Unit.Default, ApplyExt.Apply(Try.Success 3, fun _ -> ignore ()))

    [<Test>]
    let ``Applying actions to failure should result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<TheVoid.Unit> err, ApplyExt.Apply(Try.Failure<int> err, fun _ -> ignore ()))

    [<Test>]
    let ``Applying actions that throw exceptions should result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<Unit> err, ApplyExt.Apply(Try.Success 3, fun _ -> raise err))

    [<Test>]
    let ``Applying functions that return Unit and don't throw exceptions should result in success`` () =
        Assert.AreEqual(Try.Success Unit.Default, ApplyExt.ApplyWith(Try.Success 3, fun _ -> Try.Success Unit.Default))

    [<Test>]
    let ``Applying functions that return Unit to failure should result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<TheVoid.Unit> err, ApplyExt.ApplyWith(Try.Failure<int> err, fun _ -> Try.Success Unit.Default))

    [<Test>]
    let ``Applying functions that return Unit and throw exceptions should result in failure`` () =
        let err = Exception "Expected err"
        Assert.AreEqual(Try.Failure<Unit> err, ApplyExt.ApplyWith(Try.Success 3, fun _ -> raise err))