using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_divide_by_zero {
        static Func<int> _divideByZero;
        static bool _failureCallbackExecuted;
        static Exception _error;
        static ITry<int> _result;
        static int _value;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _result = Try.To(_divideByZero);

        It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.ShouldBeOfType<DivideByZeroException>();

        It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();

        It should_throw_an_NotSupportedException_when_accessing_the_value_property =
            () => Catch.Exception(() => _value = _result.Value).ShouldBeOfType<NotSupportedException>();
    }
}