using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_nested_in_a_failure {
        static Try<Try<int>> _nestedSuccess;
        static Try<int> _result;

        Establish context =
            () => { _nestedSuccess = Try.Failure(new Exception()); };

        Because of = () => { _result = _nestedSuccess.Flatten(); };

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}