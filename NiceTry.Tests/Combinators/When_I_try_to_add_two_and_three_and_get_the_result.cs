using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (GetExt), "Get")]
    internal class When_I_try_to_add_two_and_three_and_get_the_result
    {
        private static int _five;

        private Because of = () => _five = Try.To(() => 2 + 3)
            .Get();

        private It should_return_five = () => _five.Should().Be(5);
    }
}