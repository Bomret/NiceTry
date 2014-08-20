using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FilterExt), "Filter")]
    internal class When_I_check_if_a_failure_of_type_int_contains_two
    {
        private static Try<int> _failure;
        private static Try<int> _result;

        private Establish context = () => { _failure = Try.Failure(new Exception()); };

        private Because of = () => _result = _failure.Filter(i => i == 2);

        private It should_return_a_failure = () =>
            _result.IsFailure.Should().BeTrue();
    }
}