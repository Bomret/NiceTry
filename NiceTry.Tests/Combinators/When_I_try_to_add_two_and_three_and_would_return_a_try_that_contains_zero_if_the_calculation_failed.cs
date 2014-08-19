using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    class When_I_try_to_add_two_and_three_and_would_return_a_try_that_contains_zero_if_the_calculation_failed {
        static Try<int> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .OrElse(0);

        It should_contain_five_in_the_success = () => _result.Value.Should().Be(5);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}