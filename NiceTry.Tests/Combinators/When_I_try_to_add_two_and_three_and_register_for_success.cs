using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OnSuccessExt), "OnSuccess")]
    internal class When_I_try_to_add_two_and_three_and_register_for_success
    {
        private static int _five;

        private Because of = () => Try.To(() => 2 + 3)
            .OnSuccess(five => _five = five);

        private It should_return_five = () => _five.Should().Be(5);
    }
}