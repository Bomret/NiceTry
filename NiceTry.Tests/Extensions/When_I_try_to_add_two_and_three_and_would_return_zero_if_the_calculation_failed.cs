using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions))]
    internal class When_I_try_to_add_two_and_three_and_would_return_zero_if_the_calculation_failed {
        private static int _result;
        private static int _five;
        private static Func<int> _addTwoAndThree;
        private static int _zero;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _zero = 0;
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .GetOrElse(_zero);

        private It should_return_five = () => _result.ShouldEqual(_five);
    }
}