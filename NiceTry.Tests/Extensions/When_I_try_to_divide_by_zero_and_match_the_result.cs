using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"Match")]
    internal class When_I_try_to_divide_by_zero_and_match_the_result {
        private static Func<int> _divideByZero;
        private static bool _successCallbackExecuted;
        private static Exception _error;
        private static Action<int> _whenSuccess;
        private static Action<Exception> _whenFailure;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _whenSuccess = i => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        private Because of = () => Try.To(_divideByZero)
                                      .Match(_whenSuccess, _whenFailure);

        private It should_execute_the_failure_callback = () => _error.ShouldNotBeNull();

        private It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}