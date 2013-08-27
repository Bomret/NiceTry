using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_nested_in_a_failure
    {
        private static Failure<Success> _nestedSuccess;
        private static ITry _result;

        private Establish context =
            () => { _nestedSuccess = new Failure<Success>(new Exception("Expected test exception")); };

        private Because of = () => { _result = _nestedSuccess.Flatten(); };

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}