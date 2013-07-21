using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_would_return_a_try_that_contains_a_different_value_if_the_calculation_fails {
        static ITry<int> _result;
        static int _expectedResult;
        static Func<int> _add;
        static int _elseValue;

        Establish context = () => {
            _add = () => 2 + 5;
            _expectedResult = _add();

            _elseValue = 0;
        };

        Because of = () => _result = Try.To(_add)
                                        .OrElse(_elseValue);

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        It should_return_the_expected_original_value = () => _result.Value.ShouldEqual(_expectedResult);
    }
}