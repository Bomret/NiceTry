using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try))]
    public class When_I_try_to_throw_an_exception {
        private static ITry _result;
        private static Action _throwException;
        private static Exception _expectedException;

        private Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };
        };

        private Because of = () => _result = Try.To(_throwException);

        private It should_contain_the_exception_in_the_failure = () => _result.Error.ShouldEqual(_expectedException);

        private It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}