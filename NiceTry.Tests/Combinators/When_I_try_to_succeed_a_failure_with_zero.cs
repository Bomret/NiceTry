using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Succeed")]
    class When_I_try_to_succeed_a_failure_with_zero {
        static ITry<int> _failure;
        static ITry<int> _result;

        Establish context = () => _failure = new Failure<int>(new Exception());

        Because of = () => _result = _failure.Succeed(0);

        It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(0);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}