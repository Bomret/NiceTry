using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Inspect")]
    internal class
        When_I_try_to_add_two_and_three_and_throw_and_exception_when_inspecting_the_result_and_convert_it_to_a_string {
        static Func<int> _addTwoAndThree;
        static ITry<string> _result;
        static Func<int, string> _toString;
        static Action<ITry<int>> _throw;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _throw = t => { throw new Exception("Expected test exception"); };
            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Inspect(_throw)
                                        .Map(_toString);

        It should_contain_five_as_string_in_the_success =
            () => _result.Value.ShouldEqual("5");

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}