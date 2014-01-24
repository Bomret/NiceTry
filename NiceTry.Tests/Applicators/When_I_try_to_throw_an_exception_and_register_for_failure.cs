using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "OnFailure")]
    public class When_I_try_to_throw_an_exception_and_register_for_failure {
        static Exception _error;

        Because of = () => Try.To(() => { throw new Exception("Test exception"); })
                              .OnFailure(error => _error = error);

        It should_return_the_expected_exception = () => _error.Message.ShouldEqual("Test exception");
    }
}