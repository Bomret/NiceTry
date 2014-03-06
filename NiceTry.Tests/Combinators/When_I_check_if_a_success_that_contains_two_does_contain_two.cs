using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    class When_I_check_if_a_success_that_contains_two_does_contain_two {
        static Try<int> _twoSuccess;
        static Try<int> _result;

        Establish context = () => { _twoSuccess = Try.Success(2); };

        Because of = () => _result = _twoSuccess.Filter(i => i == 2);

        It should_return_the_original_success = () => _result.ShouldEqual(_twoSuccess);
    }
}