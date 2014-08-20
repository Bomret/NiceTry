using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (MatchExt), "Match")]
    internal class When_I_try_to_add_two_and_three_and_match_the_result_by_creating_a_string
    {
        private static string _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Match(i => i.ToString(),
                e => "0");

        private It should_return_five_as_string = () => _result.Should().Be("5");
    }
}