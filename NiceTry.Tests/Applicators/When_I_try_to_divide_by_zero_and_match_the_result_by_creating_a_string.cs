using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Match")]
    class When_I_try_to_divide_by_zero_and_match_the_result_by_creating_a_string {
        static string _result;
        static Func<int> _divideByZero;

        Establish ctx = () => _divideByZero = () => {
            var zero = 0;

            return 5 / zero;
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .Match(i => i.ToString(),
                                               e => "0");

        It should_return_zero_as_string = () => _result.Should().Be("0");
    }
}