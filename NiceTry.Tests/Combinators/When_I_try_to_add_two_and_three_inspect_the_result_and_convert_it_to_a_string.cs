using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Inspect")]
    internal class When_I_try_to_add_two_and_three_inspect_the_result_and_convert_it_to_a_string {
        static Func<int> _addTwoAndThree;
        static ITry<string> _result;
        static Func<int, string> _toString;
        static Action<ITry<int>> _inspect;
        static int _inspectedResult;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _inspect = t => _inspectedResult = t.Value;
            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Inspect(_inspect)
                                        .Map(_toString);

        It should_return_five_as_a_string =
            () => _result.Value.ShouldEqual("5");

        It should_set_the_result_of_the_inspection_to_five_as_int =
            () => _inspectedResult.ShouldEqual(5);
    }
}