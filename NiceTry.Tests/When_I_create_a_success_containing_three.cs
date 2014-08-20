using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Success")]
    public class When_I_create_a_success_containing_three
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.Success(3);

        private It should_contain_three_in_the_success = () => _result.Value.Should().Be(3);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}