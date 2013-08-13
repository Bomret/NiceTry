using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Map")]
    internal class When_I_try_to_add_two_and_three_and_convert_the_result_to_a_string {
        private static Func<int> _addTwoAndThree;
        private static ITry<string> _result;
        private static Func<int, string> _toString;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .Map(_toString);

        private It should_return_five_as_a_string = () => _result.Value.ShouldEqual("5");
    }
}