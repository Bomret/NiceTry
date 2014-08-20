using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_add_two_and_three
    {
        private static Try<int> _result;
        private static Exception _error;

        private Because of = () => _result = Try.To(() => 2 + 3);

        private It should_contain_five_in_the_success = () => _result.Value.Should().Be(5);

        private It should_not_contain_an_exception =
            () => Catch.Exception((Action) (() => _error = _result.Error)).Should().NotBeNull();

        private It should_not_return_a_failure = () => _result.IsFailure.Should().BeFalse();

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}