using System;
using System.Globalization;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_convert_the_result_to_a_string {
        static Func<int> _calculateEquation;
        static ITry<string> _result;
        static string _expectedResult;
        static Func<int, string> _toString;

        Establish context = () => {
            _calculateEquation = () => 2 + 3;
            _expectedResult = _calculateEquation().ToString(CultureInfo.InvariantCulture);

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        Because of = () => _result = Try.To(_calculateEquation)
                                        .Map(_toString);

        It should_return_the_expected_string_result = () => _result.Value.ShouldEqual(_expectedResult);
    }
}