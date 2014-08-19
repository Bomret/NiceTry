using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RejectExt), "Reject")]
    internal class When_I_reject_a_success_that_contains_three_if_it_would_contain_two
    {
        private static Try<int> _success;
        private static Try<int> _result;

        private Establish context = () => { _success = Try.Success(3); };

        private Because of = () => _result = _success.Reject(i => i == 2);

        private It should_contain_three_in_the_success = () => _result.Value.Should().Be(3);
        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}