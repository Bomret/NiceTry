using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (OrElseExt), "OrElse")]
    internal class When_I_try_to_divide_by_zero_and_return_a_try_that_contains_zero_instead
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.Success(0)
            .Map(zero => 5 / zero)
            .OrElse(0);

        private It should_contain_zero_in_the_success = () => _result.Value.Should().Be(0);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}