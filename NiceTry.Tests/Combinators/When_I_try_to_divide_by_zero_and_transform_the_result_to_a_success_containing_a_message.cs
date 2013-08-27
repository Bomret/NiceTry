using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    internal class When_I_try_to_divide_by_zero_and_transform_the_result_to_a_success_containing_a_message
    {
        private static ITry<string> _result;
        private static Func<int, ITry<string>> _toString;
        private static Func<Exception, ITry<string>> _returnExceptionMessage;
        private static Func<int> _divideByZero;
        private static string _expectedMessage;

        private Establish context = () =>
        {
            _divideByZero = () =>
            {
                var zero = 0;

                return 5 / zero;
            };

            _expectedMessage = "Expected test message.";

            _toString = i =>
                        Try.To(() => i.ToString(CultureInfo.InvariantCulture));
            _returnExceptionMessage = error =>
                                      new Success<string>(_expectedMessage);
        };

        private Because of = () => _result = Try.To(_divideByZero)
                                                .Transform(_toString, _returnExceptionMessage);

        private It should_contain_the_expected_message_in_the_success =
            () => _result.Value.ShouldEqual(_expectedMessage);

        private It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}