using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Applicators), "WhenComplete")]
    internal class When_I_try_to_divide_by_zero_and_register_for_completion {
        static Func<int> _divideByZero;
        static bool _failureCallbackExecuted;
        static Exception _error;
        static ITry<int> _result;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => Try.To(_divideByZero)
                              .WhenComplete(result => { _result = result; });

        It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.ShouldBeOfType<DivideByZeroException>();

        It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}