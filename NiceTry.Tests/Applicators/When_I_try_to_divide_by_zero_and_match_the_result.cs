using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Match")]
    class When_I_try_to_divide_by_zero_and_match_the_result {
        static Func<int> _divideByZero;
        static bool _successCallbackExecuted;
        static Exception _error;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => Try.To(_divideByZero)
                              .Match(i => _successCallbackExecuted = true,
                                     error => _error = error);

        It should_execute_the_failure_callback = () => _error.ShouldNotBeNull();

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}