using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    class When_I_check_if_a_success_that_contains_two_does_contain_three {
        static Try<int> _twoSuccess;
        static Try<int> _result;

        Establish context = () => { _twoSuccess = Try.Success(2); };

        Because of = () => _result = _twoSuccess.Filter(i => i == 3);

        It should_contain_an_ArgumentException_in_the_failure =
            () => _result.Error.ShouldBeOfType<ArgumentException>();

        It should_return_a_failure_success = () => _result.IsFailure.ShouldBeTrue();
    }
}