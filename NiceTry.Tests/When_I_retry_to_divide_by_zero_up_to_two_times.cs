using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_divide_by_zero_up_to_two_times {
        private static ITry<int> _result;
        private static Func<int> _divideByZero;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _result = Retry.To(_divideByZero);

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}