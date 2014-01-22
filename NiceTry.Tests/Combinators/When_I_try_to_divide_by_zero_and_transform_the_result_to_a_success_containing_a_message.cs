using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    class When_I_try_to_divide_by_zero_and_transform_the_result_to_a_success_containing_a_message {
        static ITry<string> _result;
        static Func<int> _divideByZero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .Transform(i => Try.To(() => i.ToString()),
                                                   e => new Success<string>("Test exception"));

        It should_contain_the_expected_message_in_the_success =
            () => _result.Value.ShouldEqual("Test exception");

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}