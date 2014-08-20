using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FilterExt), "Filter")]
    internal class When_I_check_if_a_success_that_contains_two_does_contain_three
    {
        private static Try<int> _twoSuccess;
        private static Try<int> _result;

        private Establish context = () => { _twoSuccess = Try.Success(2); };

        private Because of = () => _result = _twoSuccess.Filter(i => i == 3);

        private It should_contain_an_ArgumentException_in_the_failure =
            () => _result.Error.Should().BeOfType<ArgumentException>();

        private It should_return_a_failure_success = () => _result.IsFailure.Should().BeTrue();
    }
}