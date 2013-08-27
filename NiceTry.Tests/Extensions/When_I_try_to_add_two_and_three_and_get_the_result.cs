using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions
{
    [Subject(typeof (NiceTry.Extensions), "Get")]
    internal class When_I_try_to_add_two_and_three_and_get_the_result
    {
        private static Func<int> _addTwoAndThree;
        private static int _result;
        private static int _five;

        private Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .Get();

        private It should_return_five = () => _result.ShouldEqual(_five);
    }
}