using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_add_two_and_three {
        static Func<int> _addTwoAndThree;
        static ITry<int> _result;
        static int _five;
        static Exception _error;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        Because of = () => _result = Try.To(_addTwoAndThree);

        It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        It should_not_contain_an_exception = () => Catch.Exception(() => _error = _result.Error).ShouldNotBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }

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

    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_success_that_contain_the_same_value {
        static Success<int> _success;

        static bool _result;
        static Success<int> _otherSuccess;

        Establish context = () => {
            _success = new Success<int>(5);
            _otherSuccess = new Success<int>(5);
        };

        Because of = () => _result = _success.Equals(_otherSuccess);

        It should_return_true = () => _result.ShouldBeTrue();
    }

    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_success_that_contain_different_values {
        static Success<int> _success;

        static bool _result;
        static Success<int> _otherSuccess;

        Establish context = () => {
            _success = new Success<int>(5);
            _otherSuccess = new Success<int>(0);
        };

        Because of = () => _result = _success.Equals(_otherSuccess);

        It should_return_false = () => _result.ShouldBeFalse();
    }

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

    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_failure_with_a_failure_that_contain_the_same_errors {
        static Failure<int> _failure;

        static bool _result;
        static Failure<int> _otherFailure;

        Establish context = () => {
            _failure = new Failure<int>(new ArgumentException());
            _otherFailure = new Failure<int>(new ArgumentException());
        };

        Because of = () => _result = _failure.Equals(_otherFailure);

        It should_return_false = () => _result.ShouldBeFalse();
    }
}