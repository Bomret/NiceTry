using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_would_return_a_different_value_if_the_calculation_fails {
        static int _result;
        static int _expectedResult;
        static Func<int> _add;
        static int _elseValue;

        Establish context = () => {
            _add = () => 2 + 5;
            _expectedResult = _add();

            _elseValue = 0;
        };

        Because of = () => _result = Try.To(_add)
                                        .GetOrElse(_elseValue);

        It should_return_the_expected_original_value = () => _result.ShouldEqual(_expectedResult);
    }
}