using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_add_two_and_three {
        private static Func<int> _addTwoAndThree;
        private static ITry<int> _result;
        private static int _five;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        private Because of = () => _result = Try.To(_addTwoAndThree);

        private It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        private It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        private It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}