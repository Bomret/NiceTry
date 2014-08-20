using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (MapExt), "Map")]
    internal class When_I_try_to_add_two_and_three_and_convert_the_result_to_a_string
    {
        private static Try<string> _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Map(i => i.ToString());

        private It should_return_five_as_a_string = () => _result.Value.Should().Be("5");
    }
}