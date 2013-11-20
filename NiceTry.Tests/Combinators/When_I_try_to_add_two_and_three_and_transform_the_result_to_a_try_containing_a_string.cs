using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    internal class When_I_try_to_add_two_and_three_and_transform_the_result_to_a_try_containing_a_string {
        static Func<int> _addTwoAndThree;
        static ITry<string> _result;
        static string _fiveAsString;
        static Func<int, ITry<string>> _toString;
        static Func<Exception, ITry<string>> _returnExceptionMessage;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _fiveAsString = _addTwoAndThree().ToString(CultureInfo.InvariantCulture);

            _toString = i => Try.To(() => i.ToString(CultureInfo.InvariantCulture));
            _returnExceptionMessage = error => new Success<string>(error.Message);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
            .Transform(_toString, _returnExceptionMessage);

        It should_contain_five_as_a_string_in_the_success =
            () => _result.Value.ShouldEqual(_fiveAsString);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}