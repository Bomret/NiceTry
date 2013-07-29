using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_divide_by_zero_and_return_a_try_that_contains_zero_instead {
        private static Func<int> _divideByZero;
        private static ITry<int> _result;
        private static int _zero;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _zero = 0;
        };

        private Because of = () => _result = Try.To(_divideByZero)
                                                .OrElse(_zero);

        private It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(_zero);
        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}