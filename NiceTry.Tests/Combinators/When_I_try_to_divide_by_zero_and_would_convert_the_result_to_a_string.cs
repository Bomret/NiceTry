using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (MapExt), "Map")]
    internal class When_I_try_to_divide_by_zero_and_would_convert_the_result_to_a_string
    {
        private static Func<int> _divideByZero;
        private static Try<string> _result;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _result = Try.To(_divideByZero)
            .Map(i => i.ToString());

        private It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.Should().BeOfType<DivideByZeroException>();

        private It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}