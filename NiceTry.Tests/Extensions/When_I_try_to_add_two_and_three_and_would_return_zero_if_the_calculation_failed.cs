using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (NiceTry.Extensions), "GetOrElse")]
    class When_I_try_to_add_two_and_three_and_would_return_zero_if_the_calculation_failed
    {
        static int _result;
        static int _five;
        static Func<int> _addTwoAndThree;
        static int _zero;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _zero = 0;
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .GetOrElse(_zero);

        It should_return_five = () => _result.ShouldEqual(_five);
    }
}