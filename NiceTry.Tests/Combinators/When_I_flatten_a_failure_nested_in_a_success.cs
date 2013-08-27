using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_failure_nested_in_a_success
    {
        private static Success<Failure<int>> _nestedFailure;
        private static ITry<int> _result;
        private static Failure<int> _expectedResult;

        private Establish context = () =>
        {
            _expectedResult = new Failure<int>(new Exception("Expected test exception"));
            _nestedFailure = new Success<Failure<int>>(_expectedResult);
        };

        private Because of = () => { _result = _nestedFailure.Flatten(); };

        private It should_return_the_inner_failure = () => _result.ShouldEqual(_expectedResult);
    }
}