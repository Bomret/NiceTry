using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "OnSuccess")]
    class When_I_try_to_divide_by_zero_and_register_for_success {
        static Func<int> _divideByZero;
        static bool _successCallbackExecuted;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => Try.To(_divideByZero)
                              .OnSuccess(result => _successCallbackExecuted = true);

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.Should().BeFalse();
    }
}