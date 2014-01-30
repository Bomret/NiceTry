using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_failure {
        static Try<int> _success;
        static Try<int> _failure;
        static bool _result;

        Establish context = () => {
            _success = Try.Success(5);
            _failure = Try.Failure(new Exception());
        };

        Because of = () => _result = _success.Equals(_failure);

        It should_return_false = () => _result.ShouldBeFalse();
    }
}