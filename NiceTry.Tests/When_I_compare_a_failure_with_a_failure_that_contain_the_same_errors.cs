using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_failure_with_a_failure_that_contain_the_same_errors {
        static Try<int> _failure;

        static bool _result;
        static Try<int> _otherFailure;

        Establish context = () => {
            _failure = Try.Failure(new ArgumentException());
            _otherFailure = Try.Failure(new ArgumentException());
        };

        Because of = () => _result = _failure.Equals(_otherFailure);

        It should_return_false= () => _result.Should().BeFalse();
    }
}