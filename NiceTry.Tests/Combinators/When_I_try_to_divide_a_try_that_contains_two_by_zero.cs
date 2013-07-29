using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_divide_a_try_that_contains_two_by_zero {
        private static ITry<int> twoSuccess;
        private static Func<int, ITry<int>> _tryToDivideByZero;
        private static ITry<int> _result;

        private Establish context = () => {
            twoSuccess = new Success<int>(2);
            _tryToDivideByZero = i => Try.To(() => {
                var zero = 0;
                return i / zero;
            });
        };

        private Because of = () => _result = twoSuccess.FlatMap(_tryToDivideByZero);

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}