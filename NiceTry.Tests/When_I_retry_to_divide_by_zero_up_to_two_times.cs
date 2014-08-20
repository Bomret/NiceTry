using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Retry), "To")]
    internal class When_I_retry_to_divide_by_zero_up_to_two_times
    {
        private static Try<int> _result;
        private static Func<int> _divideByZero;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _result = Retry.To(_divideByZero);

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}