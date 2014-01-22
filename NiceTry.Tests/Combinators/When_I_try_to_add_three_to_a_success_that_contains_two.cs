using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "FlatMap")]
    class When_I_try_to_add_three_to_a_success_that_contains_two {
        static ITry<int> _twoSuccess;
        static ITry<int> _result;

        Establish context = () => { _twoSuccess = new Success<int>(2); };

        Because of = () => _result = _twoSuccess.FlatMap(i => Try.To(() => i + 3));

        It should_contain_five_in_the_Success = () => _result.Value.ShouldEqual(5);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}