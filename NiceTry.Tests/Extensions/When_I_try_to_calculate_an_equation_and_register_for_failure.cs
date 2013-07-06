using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_register_for_failure {
        static Func<int> _calculateEquation;
        static bool _failureCallbackExecuted;

        Establish context = () => _calculateEquation = () => 2 + 3;

        Because of = () => Try.To(_calculateEquation)
                              .WhenFailure(error => _failureCallbackExecuted = true);

        It should_not_execute_the_failure_callback = () => _failureCallbackExecuted.ShouldBeFalse();
    }
}