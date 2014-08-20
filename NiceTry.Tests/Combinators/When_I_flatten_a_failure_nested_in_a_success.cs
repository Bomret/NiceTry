using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FlattenExt), "Flatten")]
    public class When_I_flatten_a_failure_nested_in_a_success
    {
        private static Try<Try<int>> _nestedFailure;
        private static Try<int> _result;
        private static Try<int> _failure;

        private Establish context = () =>
        {
            _failure = Try.Failure(new Exception());
            _nestedFailure = Try.Success(_failure);
        };

        private Because of = () => _result = _nestedFailure.Flatten();

        private It should_return_the_inner_failure = () => _result.Should().Be(_failure);
    }
}