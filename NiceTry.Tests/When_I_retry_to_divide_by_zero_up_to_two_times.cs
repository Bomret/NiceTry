using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    class When_I_retry_to_divide_by_zero_up_to_two_times {
        static Try<int> _result;
        static Func<int> _divideByZero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _result = Retry.To(_divideByZero);

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}