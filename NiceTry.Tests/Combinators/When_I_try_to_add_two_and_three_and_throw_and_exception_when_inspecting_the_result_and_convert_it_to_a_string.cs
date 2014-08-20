using System;
using System.Globalization;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (TapExt), "Tap")]
    internal class
        When_I_try_to_add_two_and_three_and_throw_and_exception_when_inspecting_the_result_and_convert_it_to_a_string
    {
        private static Try<string> _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Tap(_ => { throw new Exception("Expected test exception"); })
            .Map(i => i.ToString(CultureInfo.InvariantCulture));

        private It should_return_a_failure =
            () => _result.IsFailure.Should().BeTrue();
    }
}