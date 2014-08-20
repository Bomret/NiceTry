using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RecoverExt), "Recover")]
    internal class When_I_try_to_add_two_and_three_and_recover_with_zero_if_any_exception_is_thrown
    {
        private static Try<int> _five;

        private Because of = () => _five = Try.To(() => 2 + 3)
            .Recover(e => 0);

        private It should_contain_five_in_the_success = () => _five.Value.Should().Be(5);

        private It should_return_a_success = () => _five.IsSuccess.Should().BeTrue();
    }
}