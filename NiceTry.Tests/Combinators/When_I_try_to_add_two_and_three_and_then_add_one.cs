using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (ThenExt), "Then")]
    internal class When_I_try_to_add_two_and_three_and_then_add_one
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Then(t => t.Value + 1);

        private It should_contain_six_in_the_success = () => _result.Value.Should().Be(6);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}