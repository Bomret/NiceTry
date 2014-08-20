using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (GetExt), "GetOrDefault")]
    internal class When_I_try_to_add_two_and_three_and_would_return_the_default_of_int_if_the_calculation_failed
    {
        private static int _five;

        private Because of = () => _five = Try.To(() => 2 + 3)
            .GetOrDefault();

        private It should_return_five = () => _five.Should().Be(5);
    }
}