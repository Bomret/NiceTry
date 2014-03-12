using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "OnFailure")]
    class When_I_try_to_add_two_and_three_and_register_for_failure {
        static bool _failureCallbackExecuted;

        Because of = () => Try.To(() => 2 + 3)
                              .OnFailure(error => _failureCallbackExecuted = true);

        It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.Should().BeFalse();
    }
}