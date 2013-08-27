using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "RecoverWith")]
    internal class When_I_try_to_throw_an_exception_and_recover_with_a_success
    {
        private static ITry _result;
        private static Action _throwException;
        private static Func<Exception, ITry> _aSuccess;

        private Establish context = () =>
        {
            _throwException = () => { throw new ArgumentException("Expected test exception."); };

            _aSuccess = error => new Success();
        };

        private Because of = () => _result = Try.To(_throwException)
                                                .RecoverWith(_aSuccess);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}