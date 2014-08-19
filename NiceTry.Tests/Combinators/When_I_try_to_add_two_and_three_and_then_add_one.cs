using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Then")]
    class When_I_try_to_add_two_and_three_and_then_add_one {
        static Try<int> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Then(t => t.Get() + 1);

        It should_contain_six_in_the_success = () => _result.Value.Should().Be(6);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}