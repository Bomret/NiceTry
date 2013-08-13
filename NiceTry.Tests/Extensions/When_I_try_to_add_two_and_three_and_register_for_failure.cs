using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"WhenFailure")]
    internal class When_I_try_to_add_two_and_three_and_register_for_failure {
        private static Func<int> _addTwoAndThree;
        private static bool _failureCallbackExecuted;

        private Establish context = () => _addTwoAndThree = () => 2 + 3;

        private Because of = () => Try.To(_addTwoAndThree)
                                      .WhenFailure(error => _failureCallbackExecuted = true);

        private It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.ShouldBeFalse();
    }
}