using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    public class When_I_try_to_throw_an_exception_and_match_the_result {
        private static Action _throwException;
        private static Exception _expectedException;
        private static Exception _error;

        private static bool _successCallbackExecuted;
        private static Action _whenSuccess;
        private static Action<Exception> _whenFailure;

        private Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };

            _whenSuccess = () => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        private Because of = () => Try.To(_throwException)
                                      .Match(_whenSuccess, _whenFailure);

        private It should_execute_the_failure_callback = () => _error.ShouldEqual(_expectedException);

        private It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}