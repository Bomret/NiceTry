namespace NiceTry.Tests.Combinators

open FsUnit
open NUnit.Framework
open NiceTry
open NiceTry.Combinators
open System
open TheVoid

module AggregateTryExt =
    [<Test>]
    let ``Aggregating an enumerable without errors should result in success``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.AggregateTry(Func<_, _, _> (+)) |> should equal (Try.Success 6)

    [<Test>]
    let ``Aggregating an enumerable using a seed without errors should result in success``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.AggregateTry(1, Func<_, _, _> (+)) |> should equal (Try.Success 7)

    [<Test>]
    let ``Aggregating an enumerable using a seed and result selector without errors should result in success``() =
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.AggregateTry(1, Func<_, _, _> (+), fun accu -> accu + 1) |> should equal (Try.Success 8)

    [<Test>]
    let ``Aggregating an enumerable with errors should result in failure``() =
        let err = InvalidOperationException "Expected err"
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        let raise = fun _ -> fun _ -> raise err
        enumerable.AggregateTry(Func<_, _, _> raise) |> should equal (Try.Failure<int> err)

    [<Test>]
    let ``Aggregating an enumerable using a seed with errors should result in failure``() =
        let err = InvalidOperationException "Expected err"
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList

        let raise =
            fun _ ->
                fun _ ->
                    raise err
                    0
        enumerable.AggregateTry(1, Func<_, _, _> raise) |> should equal (Try.Failure<int> err)

    [<Test>]
    let ``Aggregating an enumerable using a seed and result selector with errors should result in failure``() =
        let err = InvalidOperationException "Expected err"
        let enumerable = [ 1; 2; 3 ] |> Seq.ofList
        enumerable.AggregateTry(1, Func<int, int, int> (+),
                                fun _ ->
                                    raise err
                                    0)
        |> should equal (Try.Failure<int> err)