using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Reject")]
    class When_I_reject_a_failure_if_it_contains_two {
        static Try<int> _failure;
        static Try<int> _result;

        Establish context = () => { _failure = Try.Failure(new Exception()); };

        Because of = () => _result = _failure.Reject(i => i == 2);

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}