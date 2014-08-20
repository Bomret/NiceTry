using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FilterExt), "Filter")]
    internal class When_I_check_if_a_success_that_contains_two_does_contain_two
    {
        private static Try<int> _twoSuccess;
        private static Try<int> _result;

        private Establish context = () => { _twoSuccess = Try.Success(2); };

        private Because of = () => _result = _twoSuccess.Filter(i => i == 2);

        private It should_contain_two_in_the_success = () => _result.Value.Should().Be(2);
        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}