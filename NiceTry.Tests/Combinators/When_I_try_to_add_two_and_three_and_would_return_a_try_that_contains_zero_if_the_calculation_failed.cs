using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OrElseExt), "OrElse")]
    internal class When_I_try_to_add_two_and_three_and_would_return_a_try_that_contains_zero_if_the_calculation_failed
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .OrElse(0);

        private It should_contain_five_in_the_success = () => _result.Value.Should().Be(5);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}