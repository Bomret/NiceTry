using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Reject")]
    class When_I_reject_a_success_that_contains_two_if_it_contains_two {
        static Try<int> _success;
        static Try<int> _result;

        Establish context = () => { _success = Try.Success(2); };

        Because of = () => _result = _success.Reject(i => i == 2);

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}