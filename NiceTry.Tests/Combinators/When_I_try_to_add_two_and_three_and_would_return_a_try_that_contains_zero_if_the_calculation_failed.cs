using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    internal class When_I_try_to_add_two_and_three_and_would_return_a_try_that_contains_zero_if_the_calculation_failed {
        private static ITry<int> _result;
        private static int _five;
        private static Func<int> _addTwoAndThree;
        private static Success<int> _zero;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _zero = new Success<int>(0);
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .OrElse(_zero);

        private It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}