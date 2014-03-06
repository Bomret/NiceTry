using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Finally")]
    class When_I_try_to_execute_a_finally_action_on_a_success {
        static Try<int> _success;
        static Try<int> _result;
        static bool _finallyExecuted;

        Establish context = () => _success = Try.Success(5);

        Because of = () => _result = _success.Finally(() => _finallyExecuted = true);

        It should_have_executed_the_finally_action = () => _finallyExecuted.ShouldBeTrue();

        It should_return_the_original_success = () => _result.ShouldEqual(_success);
    }
}