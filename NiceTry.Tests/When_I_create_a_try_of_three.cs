using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Of")]
    public class When_I_create_a_try_of_three {
        static Try<int> _result;

        Because of = () => _result = Try.Of(3);

        It should_contain_three_in_the_success = () => _result.Value.Should().Be(3);

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}