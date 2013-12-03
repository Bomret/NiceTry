using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Applicators), "Match")]
    public class When_I_try_to_throw_an_exception_and_match_the_result {
        static Action _throwException;
        static Exception _expectedException;
        static Exception _error;

        static bool _successCallbackExecuted;
        static Action _whenSuccess;
        static Action<Exception> _whenFailure;

        Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };

            _whenSuccess = () => _successCallbackExecuted = true;
            _whenFailure = error => _error = error;
        };

        Because of = () => Try.To(_throwException)
                              .Match(_whenSuccess, _whenFailure);

        It should_execute_the_failure_callback = () => _error.ShouldEqual(_expectedException);

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}