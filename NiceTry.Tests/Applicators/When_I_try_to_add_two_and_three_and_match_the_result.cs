using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Match")]
    internal class When_I_try_to_add_two_and_three_and_match_the_result {
        static Func<int> _addTwoAndThree;
        static int _result;
        static int _five;

        static Action<int> _whenSuccess;
        static Action<Exception> _whenFailure;
        static Exception _error;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _whenSuccess = i => _result = i;
            _whenFailure = error => _error = error;
        };

        Because of = () => Try.To(_addTwoAndThree)
                              .Match(_whenSuccess, _whenFailure);

        It should_execute_the_success_callback = () => _result.ShouldEqual(_five);

        It should_not_execute_the_failure_callback = () => _error.ShouldBeNull();
    }
}