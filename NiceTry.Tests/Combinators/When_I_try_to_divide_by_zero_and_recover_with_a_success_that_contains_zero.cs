using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RecoverWithExt), "RecoverWith")]
    internal class When_I_try_to_divide_by_zero_and_recover_with_a_success_that_contains_zero
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

        private Because of = () => _result = Try.To(_divideByZero)
            .RecoverWith(e => Try.Success(0));

        private It should_contain_zero_in_the_success = () => _result.Value.Should().Be(0);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}