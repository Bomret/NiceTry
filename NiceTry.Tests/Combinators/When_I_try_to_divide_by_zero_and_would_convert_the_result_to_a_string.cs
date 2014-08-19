using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Map")]
    class When_I_try_to_divide_by_zero_and_would_convert_the_result_to_a_string {
        static Func<int> _divideByZero;
        static Try<string> _result;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .Map(i => i.ToString());

        It should_contain_a_DivideByZeroException_in_the_failure =
            () => _result.Error.Should().BeOfType<DivideByZeroException>();

        It should_return_a_failure = () => _result.IsFailure.Should().BeTrue();
    }
}