using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_failure_with_a_failure_that_contain_the_same_errors
    {
        private static Try<int> _failure;

        private static bool _result;
        private static Try<int> _otherFailure;

        private Establish context = () =>
        {
            _failure = Try.Failure(new ArgumentException());
            _otherFailure = Try.Failure(new ArgumentException());
        };

        private Because of = () => _result = _failure.Equals(_otherFailure);

        private It should_return_false = () => _result.Should().BeFalse();
    }
}