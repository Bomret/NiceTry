using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_divide_a_try_that_contains_two_by_zero {
        static ITry<int> twoSuccess;
        static Func<int, ITry<int>> _tryToDivideByZero;
        static ITry<int> _result;

        Establish context = () => {
            twoSuccess = new Success<int>(2);
            _tryToDivideByZero = i => Try.To(() => {
                var zero = 0;
                return i / zero;
            });
        };

        Because of = () => _result = twoSuccess.FlatMap(_tryToDivideByZero);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}