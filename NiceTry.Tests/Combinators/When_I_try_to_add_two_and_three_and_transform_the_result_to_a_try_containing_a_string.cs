using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    internal class When_I_try_to_add_two_and_three_and_transform_the_result_to_a_try_containing_a_string {
        private static Func<int> _addTwoAndThree;
        private static ITry<string> _result;
        private static string _fiveAsString;
        private static Func<int, ITry<string>> _toString;
        private static Func<Exception, ITry<string>> _returnExceptionMessage;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _fiveAsString = _addTwoAndThree().ToString(CultureInfo.InvariantCulture);

            _toString = i => Try.To(() => i.ToString(CultureInfo.InvariantCulture));
            _returnExceptionMessage = error => new Success<string>(error.Message);
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .Transform(_toString, _returnExceptionMessage);

        private It should_contain_five_as_a_string_in_the_success =
            () => _result.Value.ShouldEqual(_fiveAsString);

        private It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}