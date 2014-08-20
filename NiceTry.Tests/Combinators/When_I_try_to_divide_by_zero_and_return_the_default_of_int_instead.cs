using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (GetExt), "GetOrDefault")]
    internal class When_I_try_to_divide_by_zero_and_return_the_default_of_int_instead
    {
        private static Func<int> _divideByZero;
        private static int _zero;

        private Establish ctx = () => _divideByZero = () =>
        {
            var zero = 0;

            return 5 / zero;
        };

        private Because of = () => _zero = Try.To(_divideByZero)
            .GetOrDefault();

        private It should_return_five = () => _zero.Should().Be(default(int));
    }
}