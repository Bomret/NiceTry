using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_divide_by_zero_and_return_a_different_value_else_instead {
        static Func<int> _divideByZero;
        static int _result;
        static int _expectedResult;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _expectedResult = 0;
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .GetOrElse(_expectedResult);

        It should_return_the_expected_else_value = () => _result.ShouldEqual(_expectedResult);
    }
}