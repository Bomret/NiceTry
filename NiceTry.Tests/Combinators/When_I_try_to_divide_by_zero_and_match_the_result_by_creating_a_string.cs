using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (MatchExt), "Match")]
    internal class When_I_try_to_divide_by_zero_and_match_the_result_by_creating_a_string
    {
        private static string _result;
        private static Func<int> _divideByZero;

        private Establish ctx = () => _divideByZero = () =>
        {
            var zero = 0;

            return 5 / zero;
        };

        private Because of = () => _result = Try.To(_divideByZero)
            .Match(i => i.ToString(),
                e => "0");

        private It should_return_zero_as_string = () => _result.Should().Be("0");
    }
}