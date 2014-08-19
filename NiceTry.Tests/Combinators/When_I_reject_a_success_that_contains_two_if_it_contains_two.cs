using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RejectExt), "Reject")]
    internal class When_I_reject_a_success_that_contains_two_if_it_contains_two
    {
        private static Try<int> _success;
        private static Try<int> _result;

        private Establish context = () => { _success = Try.Success(2); };

        private Because of = () => _result = _success.Reject(i => i == 2);

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}