using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "WhenComplete")]
    class When_I_try_to_add_two_and_three_and_register_for_completion {
        static Func<int> _addTwoAndThree;
        static ITry<int> _result;
        static int _expectedResult;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _expectedResult = _addTwoAndThree();
        };

        Because of = () => Try.To(_addTwoAndThree)
                              .WhenComplete(result => _result = result);

        It should_contain_the_expected_result_in_the_success = () => _result.Value.ShouldEqual(_expectedResult);

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}