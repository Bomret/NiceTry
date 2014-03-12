using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "FromValue")]
    public class When_I_try_to_create_a_success_from_five {
        static Try<int> _result;
        static Exception _error;

        Because of = () => _result = Try.Success(5);

        It should_contain_five_in_the_success = () => _result.Value.Should().Be(5);

        It should_not_contain_an_exception = () => Catch.Exception(() => _error = _result.Error).Should().NotBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.Should().BeFalse();

        It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}