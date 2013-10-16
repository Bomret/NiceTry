using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "RecoverWith")]
    class When_I_try_to_throw_an_exception_and_recover_with_a_success
    {
        static ITry _result;
        static Action _throwException;
        static Func<Exception, ITry> _aSuccess;

        Establish context = () =>
        {
            _throwException = () => { throw new ArgumentException("Expected test exception."); };

            _aSuccess = error => new Success();
        };

        Because of = () => _result = Try.To(_throwException)
                                        .RecoverWith(_aSuccess);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}