using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    public class When_I_try_to_throw_an_exception_and_register_for_success {
        private static Action _throwException;
        private static bool _successCallbackExecuted;

        private Establish context =
            () => { _throwException = () => { throw new ArgumentException("Expected test exception"); }; };

        private Because of = () => Try.To(_throwException)
                                      .WhenSuccess(() => _successCallbackExecuted = true);

        private It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}