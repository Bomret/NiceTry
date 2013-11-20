using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    internal class When_I_try_to_throw_an_exception_and_transform_the_result_to_a_success {
        static Func<ITry> _returnFailure;
        static Func<Exception, ITry> _fromErrorToSuccess;
        static ITry _result;
        static Action _throwException;

        Establish context = () => {
            _throwException = () => {
                throw new ArgumentException("Test exception.");
            };

            _returnFailure = () => new Failure(new ArgumentException("Unexpected test exception."));
            _fromErrorToSuccess = error => new Success();
        };

        Because of = () => _result = Try.To(_throwException)
            .Transform(_returnFailure, _fromErrorToSuccess);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}