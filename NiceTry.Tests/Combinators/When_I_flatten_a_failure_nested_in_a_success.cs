using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_failure_nested_in_a_success {
        static Success<Failure<int>> _nestedFailure;
        static ITry<int> _result;
        static Failure<int> _expectedResult;

        Establish context = () => {
            _expectedResult = new Failure<int>(new Exception("Expected test exception"));
            _nestedFailure = new Success<Failure<int>>(_expectedResult);
        };

        Because of = () => {
            _result = _nestedFailure.Flatten();
        };

        It should_return_the_inner_failure = () => _result.ShouldEqual(_expectedResult);
    }
}