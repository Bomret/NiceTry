using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_that_contains_zero_nested_in_a_failure
    {
        private static Failure<Success<int>> _nestedSuccess;
        private static ITry<int> _result;

        private Establish context =
            () => { _nestedSuccess = new Failure<Success<int>>(new Exception("Expected test exception")); };

        private Because of = () => { _result = _nestedSuccess.Flatten(); };

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}