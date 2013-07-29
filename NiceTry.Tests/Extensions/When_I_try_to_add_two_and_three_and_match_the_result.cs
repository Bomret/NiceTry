using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    internal class When_I_try_to_add_two_and_three_and_match_the_result {
        private static Func<int> _addTwoAndThree;
        private static int _result;
        private static int _five;

        private static Action<int> _whenSuccess;
        private static Action<Exception> _whenFailure;
        private static Exception _error;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _whenSuccess = i => _result = i;
            _whenFailure = error => _error = error;
        };

        private Because of = () => Try.To(_addTwoAndThree)
                                      .Match(_whenSuccess, _whenFailure);

        private It should_execute_the_success_callback = () => _result.ShouldEqual(_five);
        private It should_not_execute_the_failure_callback = () => _error.ShouldBeNull();
    }
}