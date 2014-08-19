using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_divide_by_zero {
        static Func<int> _divideByZero;
        static bool _failureCallbackExecuted;
        static Exception _error;
        static Try<int> _result;
        static int _value;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _result = Try.To(_divideByZero);

        It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.Should().BeOfType<DivideByZeroException>();

        It should_not_return_a_success = () => _result.IsSuccess.Should().BeFalse();

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();

        It should_throw_an_NotSupportedException_when_accessing_the_value_property =
            () => Catch.Exception(() => _value = _result.Value).Should().BeOfType<InvalidOperationException>();
    }
}