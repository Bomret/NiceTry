using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    internal class When_I_try_to_add_two_and_three_and_would_return_a_try_that_contains_zero_if_the_calculation_failed {
        static ITry<int> _result;
        static int _five;
        static Func<int> _addTwoAndThree;
        static Success<int> _zero;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _zero = new Success<int>(0);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .OrElse(_zero);

        It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}