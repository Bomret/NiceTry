using System.Globalization;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FinallyExt), "Finally")]
    internal class
        When_I_try_to_add_two_and_three_and_convert_it_to_a_string_and_finally_set_a_bool
    {
        private static Try<string> _result;
        private static bool _isSet;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Map(i => i.ToString(CultureInfo.InvariantCulture))
            .Finally(() => _isSet = true);

        private It should_contain_five_as_string_in_the_success =
            () => _result.Value.Should().Be("5");

        private It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();

        private It should_set_the_bool =
            () => _isSet.Should().BeTrue();
    }
}