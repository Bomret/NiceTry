using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"WhenComplete")]
    internal class When_I_try_to_add_two_and_three_and_register_for_completion {
        private static Func<int> _addTwoAndThree;
        private static ITry<int> _result;
        private static int _expectedResult;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _expectedResult = _addTwoAndThree();
        };

        private Because of = () => Try.To(_addTwoAndThree)
                                      .WhenComplete(result => _result = result);

        private It should_contain_the_expected_result_in_the_success = () => _result.Value.ShouldEqual(_expectedResult);

        private It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        private It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}