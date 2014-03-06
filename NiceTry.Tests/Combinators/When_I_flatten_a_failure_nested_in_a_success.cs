using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_failure_nested_in_a_success {
        static Try<Try<int>> _nestedFailure;
        static Try<int> _result;
        static Try<int> _failure;

        Establish context = () => {
            _failure = Try.Failure(new Exception());
            _nestedFailure = Try.Success(_failure);
        };

        Because of = () => _result = _nestedFailure.Flatten();

        It should_return_the_inner_failure = () => _result.ShouldEqual(_failure);
    }
}