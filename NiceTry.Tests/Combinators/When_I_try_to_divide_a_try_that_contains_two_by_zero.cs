using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "FlatMap")]
    class When_I_try_to_divide_a_try_that_contains_two_by_zero {
        static Try<int> twoSuccess;
        static Func<int, Try<int>> _tryToDivideByZero;
        static Try<int> _result;

        Establish context = () => {
            twoSuccess = Try.Success(2);
            _tryToDivideByZero = i => Try.To(() => {
                var zero = 0;
                return i / zero;
            });
        };

        Because of = () => _result = twoSuccess.FlatMap(_tryToDivideByZero);

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}