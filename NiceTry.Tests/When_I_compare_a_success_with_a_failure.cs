using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_failure
    {
        private static Try<int> _success;
        private static Try<int> _failure;
        private static bool _result;

        private Establish context = () =>
        {
            _success = Try.Success(5);
            _failure = Try.Failure(new Exception());
        };

        private Because of = () => _result = _success.Equals(_failure);

        private It should_return_false = () => _result.Should().BeFalse();
    }
}