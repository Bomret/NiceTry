using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RejectExt), "Reject")]
    internal class When_I_reject_a_failure_if_it_contains_two
    {
        private static Try<int> _failure;
        private static Try<int> _result;

        private Establish context = () => { _failure = Try.Failure(new Exception()); };

        private Because of = () => _result = _failure.Reject(i => i == 2);

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}