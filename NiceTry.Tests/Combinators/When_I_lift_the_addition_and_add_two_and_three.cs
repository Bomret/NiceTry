using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "LiftMap")]
    class When_I_lift_the_addition_and_add_two_and_three
    {
        static Success<int> _two;
        static Success<int> _three;
        static Func<int, int, int> _add;
        static ITry<int> _five;

        Establish context = () =>
        {
            _two = new Success<int>(2);
            _three = new Success<int>(3);
            _add = (a, b) => a + b;
        };

        Because of = () => _five = _two.LiftMap(_three, _add);

        It should_contain_five_in_the_success = () => _five.Value.ShouldEqual(5);

        It should_return_a_success = () => _five.IsSuccess.ShouldBeTrue();
    }
}