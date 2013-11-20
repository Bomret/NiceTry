using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Apply")]
    internal class When_I_try_to_add_two_and_three_and_set_a_field_to_the_result {
        static Func<int> _addTwoAndThree;
        static ITry _result;
        static Action<int> _setField;
        static int _five;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;

            _setField = i => _five = i;
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
            .Apply(_setField);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
        It should_set_the_field_to_five = () => _five.ShouldEqual(5);
    }
}