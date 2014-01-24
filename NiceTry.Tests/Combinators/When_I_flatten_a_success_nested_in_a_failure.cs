using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_nested_in_a_failure {
        static Failure<Success<int>> _nestedSuccess;
        static ITry<int> _result;

        Establish context =
            () => { _nestedSuccess = new Failure<Success<int>>(new Exception()); };

        Because of = () => { _result = _nestedSuccess.Flatten(); };

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}