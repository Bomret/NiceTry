using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (MatchExt), "Match")]
    internal class When_I_try_to_add_two_and_three_and_match_the_result
    {
        private static Exception _error;
        private static int _five;

        private Because of = () => Try.To(() => 2 + 3)
            .Match(i => _five = i,
                e => _error = e);

        private It should_execute_the_success_callback = () => _five.Should().Be(5);

        private It should_not_execute_the_failure_callback = () => _error.Should().BeNull();
    }
}