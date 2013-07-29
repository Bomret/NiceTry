using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try))]
    public class When_I_try_to_divide_by_zero {
        private static Func<int> _divideByZero;
        private static bool _failureCallbackExecuted;
        private static Exception _error;
        private static ITry<int> _result;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _result = Try.To(_divideByZero);

        private It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.ShouldBeOfType<DivideByZeroException>();

        private It should_contain_a_value_that_matches_the_value_types_default =
            () => _result.Value.ShouldEqual(default(int));

        private It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}