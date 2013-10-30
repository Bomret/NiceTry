using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "AndThen")]
    class When_I_try_to_add_two_and_three_and_then_convert_the_result_to_a_string
    {
        static Func<int> _addTwoAndThree;
        static Func<ITry<int>, ITry<string>> _toString;
        static ITry<string> _result;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _toString = t => t.Map(i => i.ToString());
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Then(_toString);

        It should_contain_five_as_string_in_the_success = () => _result.Value.ShouldEqual("5");

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}