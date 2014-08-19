using System;
using System.Globalization;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Tap")]
    class When_I_try_to_add_two_and_three_and_throw_and_exception_when_inspecting_the_result_and_convert_it_to_a_string {
        static Try<string> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Tap(_ => { throw new Exception("Expected test exception"); })
                                        .Map(i => i.ToString(CultureInfo.InvariantCulture));

        It should_contain_five_as_string_in_the_success =
            () => _result.Value.Should().Be("5");

        It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}