using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "RecoverWith")]
    class
        When_I_try_to_add_two_and_three_and_recover_with_a_success_that_contains_zero_if_any_exception_is_thrown {
        static Try<int> _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .RecoverWith(e => Try.Success(0));

        It should_contain_five_in_the_success = () => _five.Value.Should().Be(5);

        It should_return_a_success = () => _five.IsSuccess.Should().BeTrue();
    }
}