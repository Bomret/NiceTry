using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (Applicators), "WhenSuccess")]
    internal class When_I_try_to_divide_by_zero_and_register_for_success
    {
        private static Func<int> _divideByZero;
        private static bool _successCallbackExecuted;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => Try.To(_divideByZero)
                                      .WhenSuccess(result => _successCallbackExecuted = true);

        private It should_not_execute_the_success_callback = () => _successCallbackExecuted.ShouldBeFalse();
    }
}