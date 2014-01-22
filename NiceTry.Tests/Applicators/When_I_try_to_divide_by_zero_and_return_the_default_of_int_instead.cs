using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "GetOrDefault")]
    class When_I_try_to_divide_by_zero_and_return_the_default_of_int_instead {
        static Func<int> _divideByZero;
        static int _zero;

        Establish ctx = () => _divideByZero = () => {
            var zero = 0;

            return 5 / zero;
        };

        Because of = () => _zero = Try.To(_divideByZero)
                                      .GetOrDefault();

        It should_return_five = () => _zero.ShouldEqual(default(int));
    }
}