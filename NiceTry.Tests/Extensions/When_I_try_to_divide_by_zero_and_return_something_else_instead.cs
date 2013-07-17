using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    internal class When_I_try_to_divide_by_zero_and_return_something_else_instead {
        static Func<int> _divideByZero;
        static ITry<int> _result;
        static int _expectedResult;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _expectedResult = 0;
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .OrElse(_expectedResult);

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        It should_return_the_expected_else_value = () => _result.Value.ShouldEqual(_expectedResult);
    }
}