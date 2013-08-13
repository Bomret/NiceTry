using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"WhenFailure")]
    public class When_I_try_to_throw_an_exception_and_register_for_failure {
        private static Action _throwException;
        private static Exception _expectedException;
        private static Exception _error;

        private Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };
        };

        private Because of = () => Try.To(_throwException)
                                      .WhenFailure(error => _error = error);

        private It should_return_the_expected_exception = () => _error.ShouldEqual(_expectedException);
    }
}