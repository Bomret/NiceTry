using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OnFailureExt), "OnFailure")]
    internal class When_I_try_to_add_two_and_three_and_register_for_failure
    {
        private static bool _failureCallbackExecuted;

        private Because of = () => Try.To(() => 2 + 3)
            .OnFailure(error => _failureCallbackExecuted = true);

        private It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.Should().BeFalse();
    }
}