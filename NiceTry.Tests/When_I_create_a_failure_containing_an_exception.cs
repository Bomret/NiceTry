using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Failure")]
    public class When_I_create_a_failure_containing_an_exception
    {
        private static Try<int> _result;

        private Because of = () => _result = Try.Failure(new ArgumentException());

        private It should_contain_the_exception_in_the_failure = () => _result.Error.Should().NotBeNull();

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}