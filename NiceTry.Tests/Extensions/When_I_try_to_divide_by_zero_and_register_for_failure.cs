using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (NiceTry.Extensions), "WhenFailure")]
    class When_I_try_to_divide_by_zero_and_register_for_failure
    {
        static Func<int> _divideByZero;
        static Exception _error;

        Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => Try.To(_divideByZero)
                              .WhenFailure(error => _error = error);

        It should_return_a_DivideByZeroException = () => _error.ShouldBeOfType<DivideByZeroException>();
    }
}