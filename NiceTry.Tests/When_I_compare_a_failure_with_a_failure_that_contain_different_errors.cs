using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_failure_with_a_failure_that_contain_different_errors {
        static Failure<int> _failure;

        static bool _result;
        static Failure<int> _otherFailure;

        Establish context = () => {
            _failure = new Failure<int>(new ArgumentException());
            _otherFailure = new Failure<int>(new IndexOutOfRangeException());
        };

        Because of = () => _result = _failure.Equals(_otherFailure);

        It should_return_false = () => _result.ShouldBeFalse();
    }
}