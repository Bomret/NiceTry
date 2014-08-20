using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (TransformExt), "Transform")]
    internal class When_I_try_to_divide_by_zero_and_transform_the_result_to_a_success_containing_a_message
    {
        private static Try<string> _result;
        private static Func<int> _divideByZero;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };
        };

        private Because of = () => _result = Try.To(_divideByZero)
            .Transform(i => i.ToString(),
                e => "Test exception");

        private It should_contain_the_expected_message_in_the_success =
            () => _result.Value.Should().Be("Test exception");

        private It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}