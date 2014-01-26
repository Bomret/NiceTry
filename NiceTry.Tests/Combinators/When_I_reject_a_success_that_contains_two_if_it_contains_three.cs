using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Reject")]
    class When_I_reject_a_success_that_contains_two_if_it_contains_three {
        static ITry<int> _success;
        static ITry<int> _result;

        Establish context = () => { _success = Try.FromValue(3); };

        Because of = () => _result = _success.Reject(i => i == 2);

        It should_return_the_original_success = () => _result.ShouldBeTheSameAs(_success);
    }
}