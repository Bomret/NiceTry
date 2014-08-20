using System;
using System.Globalization;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FinallyExt), "Finally")]
    internal class
        When_I_try_to_add_two_and_three_and_convert_it_to_a_string_and_finally_throw_an_exception
    {
        private static Try<string> _result;

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Map(i => i.ToString(CultureInfo.InvariantCulture))
            .Finally(() => { throw new Exception("expected test exception"); });

        private It should_return_a_failure =
            () => _result.IsFailure.Should().BeTrue();
    }
}