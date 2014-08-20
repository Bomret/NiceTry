using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (GetExt), "GetOrElse")]
    internal class When_I_try_to_divide_by_zero_and_return_zero_instead
    {
        private static Func<int> _divideByZero;
        private static int _zero;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _zero = Try.To(_divideByZero)
            .GetOrElse(0);

        private It should_return_zero = () => _zero.Should().Be(0);
    }
}