using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElseWith")]
    class When_I_try_to_divide_by_zero_finish_with_minus_one_instead {
        static Try<int> _result;

        Because of = () => _result = Try.Success(0)
                                        .Map(zero => 5 / zero)
                                        .OrElseWith(Try.Success(-1));

        It should_contain_four_in_the_success = () => _result.Value.Should().Be(-1);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}