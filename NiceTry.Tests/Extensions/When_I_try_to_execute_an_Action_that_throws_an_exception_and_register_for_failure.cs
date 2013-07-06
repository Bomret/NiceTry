using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Try))]
    public class When_I_try_to_execute_an_Action_that_throws_an_exception_and_register_for_failure {
        static Action _throwException;
        static Exception _expectedException;
        static Exception _error;

        Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };
        };

        Because of = () => Try.To(_throwException)
                              .WhenFailure(error => _error = error);

        It should_return_the_expected_exception = () => _error.ShouldEqual(_expectedException);
    }
}