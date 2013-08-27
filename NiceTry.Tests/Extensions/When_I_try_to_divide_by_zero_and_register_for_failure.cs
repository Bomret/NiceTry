using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (NiceTry.Extensions), "WhenFailure")]
    internal class When_I_try_to_divide_by_zero_and_register_for_failure
    {
        private static Func<int> _divideByZero;
        private static Exception _error;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => Try.To(_divideByZero)
                                      .WhenFailure(error => _error = error);

        private It should_return_a_DivideByZeroException = () => _error.ShouldBeOfType<DivideByZeroException>();
    }
}