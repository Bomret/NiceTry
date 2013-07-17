using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    internal class When_I_try_to_calculate_an_equation_and_would_calculate_something_else_if_the_calculation_fails {
        static ITry<int> _result;
        static int _expectedResult;
        static Func<int> _calculateSomethingElse;
        static Func<int> _add;

        Establish context = () => {
            _add = () => 2 + 5;
            _expectedResult = _add();

            _calculateSomethingElse = () => 2 + 5;
        };

        Because of = () => _result = Try.To(_add)
                                        .OrElse(_calculateSomethingElse);

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        It should_return_the_expected_original_value = () => _result.Value.ShouldEqual(_expectedResult);
    }
}