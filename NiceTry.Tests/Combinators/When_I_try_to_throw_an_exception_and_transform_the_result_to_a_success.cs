using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_throw_an_exception_and_transform_the_result_to_a_success {
        private static Func<ITry> _returnFailure;
        private static Func<Exception, ITry> _fromErrorToSuccess;
        private static ITry _result;
        private static Action _throwException;

        private Establish context = () => {
            _throwException = () => { throw new ArgumentException("Test exception."); };

            _returnFailure = () => new Failure(new ArgumentException("Unexpected test exception."));
            _fromErrorToSuccess = error => new Success();
        };

        private Because of = () => _result = Try.To(_throwException)
                                                .Transform(_returnFailure, _fromErrorToSuccess);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}