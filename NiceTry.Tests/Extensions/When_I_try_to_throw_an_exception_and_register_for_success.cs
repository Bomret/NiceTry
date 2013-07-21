using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    public class When_I_try_to_throw_an_exception_and_register_for_success {
        static Action _throwException;
        static bool _successCallbackExecuted;

        Establish context =
            () => { _throwException = () => { throw new ArgumentException("Expected test exception"); }; };

        Because of = () => Try.To(_throwException)
                              .WhenSuccess(() => _successCallbackExecuted = true);

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}