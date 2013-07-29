using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_divide_by_zero_and_recover_with_zero_if_any_exception_is_thrown {
        private static ITry<int> _result;
        private static Func<int> _divideByZero;
        private static Func<Exception, int> _withZero;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _withZero = error => 0;
        };

        private Because of = () => _result = Try.To(_divideByZero)
                                                .Recover(_withZero);

        private It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(0);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}