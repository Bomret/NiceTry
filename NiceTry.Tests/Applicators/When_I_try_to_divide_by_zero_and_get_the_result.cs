using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Get")]
    internal class When_I_try_to_divide_by_zero_and_get_the_result {
        static Func<int> _divideByZero;
        static bool _successCallbackExecuted;
        static Exception _error;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _error = Catch.Exception(() => Try.To(_divideByZero)
                                                             .Get());

        It should_throw_the_DivideByZeroException_contained_in_the_try =
            () => _error.ShouldBeOfType<DivideByZeroException>();
    }
}