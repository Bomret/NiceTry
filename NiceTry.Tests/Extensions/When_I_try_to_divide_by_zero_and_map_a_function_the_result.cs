using System;
using System.Globalization;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_divide_by_zero_and_map_a_function_the_result {
        static Func<int> _divideByZero;
        static Func<int, string> _toString;
        static ITry<string> _result;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .Map(_toString);

        It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.ShouldBeOfType<DivideByZeroException>();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}