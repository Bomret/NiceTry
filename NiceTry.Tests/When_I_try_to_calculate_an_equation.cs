using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try))]
    public class When_I_try_to_calculate_an_equation {
        static Func<int> _calculateEquation;
        static ITry<int> _result;
        static int _expectedResult;

        Establish context = () => {
            _calculateEquation = () => 2 + 3;
            _expectedResult = _calculateEquation();
        };

        Because of = () => _result = Try.To(_calculateEquation);

        It should_contain_the_expected_result_in_the_success = () => _result.Value.ShouldEqual(_expectedResult);

        It should_not_contain_an_exception = () => _result.Error.ShouldBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}