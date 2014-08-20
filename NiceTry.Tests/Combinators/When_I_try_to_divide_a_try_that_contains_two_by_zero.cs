using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FlatMapExt), "FlatMap")]
    internal class When_I_try_to_divide_a_try_that_contains_two_by_zero
    {
        private static Try<int> twoSuccess;
        private static Func<int, Try<int>> _tryToDivideByZero;
        private static Try<int> _result;

        private Establish context = () =>
        {
            twoSuccess = Try.Success(2);
            _tryToDivideByZero = i => Try.To(() =>
            {
                var zero = 0;
                return i / zero;
            });
        };

        private Because of = () => _result = twoSuccess.FlatMap(_tryToDivideByZero);

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}