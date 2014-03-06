using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Reject")]
    class When_I_reject_a_success_that_contains_three_if_it_would_contain_two {
        static Try<int> _success;
        static Try<int> _result;

        Establish context = () => { _success = Try.Success(3); };

        Because of = () => _result = _success.Reject(i => i == 2);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        It should_contain_three_in_the_success = () => _result.Value.ShouldEqual(3);
    }
}