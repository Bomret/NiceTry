using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    internal class When_I_try_to_add_two_and_three_and_register_for_failure {
        static Func<int> _addTwoAndThree;
        static bool _failureCallbackExecuted;

        Establish context = () => _addTwoAndThree = () => 2 + 3;

        Because of = () => Try.To(_addTwoAndThree)
                              .WhenFailure(error => _failureCallbackExecuted = true);

        It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.ShouldBeFalse();
    }
}