using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Apply")]
    internal class When_I_try_to_add_two_and_three_and_set_a_field_to_the_result
    {
        private static Func<int> _addTwoAndThree;
        private static ITry _result;
        private static Action<int> _setField;
        private static int _five;

        private Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;

            _setField = i => _five = i;
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .Apply(_setField);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
        private It should_set_the_field_to_five = () => _five.ShouldEqual(5);
    }
}