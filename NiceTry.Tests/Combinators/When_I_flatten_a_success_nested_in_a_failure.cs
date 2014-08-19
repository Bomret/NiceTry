using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FlattenExt), "Flatten")]
    public class When_I_flatten_a_success_nested_in_a_failure
    {
        private static Try<Try<int>> _nestedSuccess;
        private static Try<int> _result;

        private Establish context =
            () => { _nestedSuccess = Try.Failure(new Exception()); };

        private Because of = () => { _result = _nestedSuccess.Flatten(); };

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}