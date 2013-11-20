using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_throw_an_exception {
        static ITry _result;
        static Action _throwException;
        static Exception _expectedException;

        Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => {
                throw _expectedException;
            };
        };

        Because of = () => _result = Try.To(_throwException);

        It should_contain_the_exception_in_the_failure = () => _result.Error.ShouldEqual(_expectedException);

        It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}