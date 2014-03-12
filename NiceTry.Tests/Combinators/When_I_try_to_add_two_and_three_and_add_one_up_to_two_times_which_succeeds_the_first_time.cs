using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Retry")]
    class When_I_try_to_add_two_and_three_and_add_one_up_to_two_times_which_succeeds_the_first_time {
        static Try<int> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Retry(i => i + 1, 2);

        It should_contain_six_in_the_success =
            () => _result.Value.Should().Be(6);

        It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}