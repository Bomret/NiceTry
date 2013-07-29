using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_divide_by_zero_and_would_convert_the_result_to_a_string {
        private static Func<int> _divideByZero;
        private static Func<int, string> _toString;
        private static ITry<string> _result;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        private Because of = () => _result = Try.To(_divideByZero)
                                                .Map(_toString);

        private It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.ShouldBeOfType<DivideByZeroException>();

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}