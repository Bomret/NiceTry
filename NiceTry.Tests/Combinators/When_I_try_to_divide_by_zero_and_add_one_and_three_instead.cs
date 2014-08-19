using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    class When_I_try_to_divide_by_zero_and_add_one_and_three_instead {
        static Try<int> _result;

        Because of = () => _result = Try.Success(0)
                                        .Map(zero => 5 / zero)
                                        .OrElse(1 + 3);

        It should_contain_four_in_the_success = () => _result.Value.Should().Be(4);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}