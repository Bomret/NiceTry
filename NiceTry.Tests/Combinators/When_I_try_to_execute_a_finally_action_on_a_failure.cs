using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Finally")]
    class When_I_try_to_execute_a_finally_action_on_a_failure {
        static ITry<int> _failure;
        static ITry<int> _result;
        static bool _finallyExecuted;

        Establish context = () => _failure = new Failure<int>(new Exception());

        Because of = () => _result = _failure.Finally(() => _finallyExecuted = true);

        It should_have_executed_the_finally_action = () => _finallyExecuted.ShouldBeTrue();

        It should_return_the_original_failure = () => _result.ShouldEqual(_failure);
    }
}