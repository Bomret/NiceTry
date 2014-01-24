using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Fail")]
    class When_I_try_to_fail_a_success_with_an_exception {
        static ITry<int> _success;
        static ITry<int> _result;

        Establish context = () => _success = new Success<int>(5);

        Because of = () => _result = _success.Fail(new Exception("test exception"));

        It should_contain_the_expected_exception_in_the_failure =
            () => _result.Error.Message.ShouldEqual("test exception");

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}