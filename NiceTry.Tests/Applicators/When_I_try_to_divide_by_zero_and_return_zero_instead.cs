using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "GetOrElse")]
    class When_I_try_to_divide_by_zero_and_return_zero_instead {
        static Func<int> _divideByZero;
        static int _zero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _zero = Try.To(_divideByZero)
                                      .GetOrElse(0);

        It should_return_zero = () => _zero.Should().Be(0);
    }
}