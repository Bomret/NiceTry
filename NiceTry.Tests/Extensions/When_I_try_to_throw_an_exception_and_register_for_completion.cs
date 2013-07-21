using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    public class When_I_try_to_throw_an_exception_and_register_for_completion {
        static ITry _result;
        static Action _throwException;
        static Exception _expectedException;

        Establish context = () => {
            _expectedException = new ArgumentException("Expected test exception");

            _throwException = () => { throw _expectedException; };
        };

        Because of = () => Try.To(_throwException)
                              .WhenComplete(result => _result = result);

        It should_contain_the_expected_exception_in_the_success = () => _result.Error.ShouldEqual(_expectedException);

        It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}