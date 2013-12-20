using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_failure {
        static Success<int> _success;
        static Failure<int> _failure;
        static bool _result;

        Establish context = () => {
            _success = new Success<int>(5);
            _failure = new Failure<int>(new Exception());
        };

        Because of = () => _result = _success.Equals(_failure);

        It should_return_false = () => _result.ShouldBeFalse();
    }
}