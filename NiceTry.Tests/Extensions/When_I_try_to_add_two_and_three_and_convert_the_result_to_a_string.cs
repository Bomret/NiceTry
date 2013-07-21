using System;
using System.Globalization;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Combinators))]
    internal class When_I_try_to_add_two_and_three_and_convert_the_result_to_a_string {
        static Func<int> _addTwoAndThree;
        static ITry<string> _result;
        static string _fiveAsString;
        static Func<int, string> _toString;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _fiveAsString = _addTwoAndThree().ToString(CultureInfo.InvariantCulture);

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Map(_toString);

        It should_return_the_five_as_a_string = () => _result.Value.ShouldEqual(_fiveAsString);
    }
}