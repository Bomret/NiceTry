using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    internal class When_I_retry_to_add_two_and_three_up_to_two_times_which_succeeds_the_first_time {
        private static ITry<int> _result;
        private static int _five;
        private static Func<int> _addTwoAndThree;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        private Because of = () => _result = Retry.To(_addTwoAndThree);

        private It should_contain_five_in_the_success =
            () => _result.Value.ShouldEqual(_five);

        private It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}