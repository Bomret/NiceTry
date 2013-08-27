using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_that_contains_zero_and_is_nested_in_a_success
    {
        private static Success<Success<int>> _nestedSuccess;
        private static ITry<int> _result;
        private static Success<int> _expectedResult;

        private Establish context = () =>
        {
            _expectedResult = new Success<int>(0);
            _nestedSuccess = new Success<Success<int>>(_expectedResult);
        };

        private Because of = () => { _result = _nestedSuccess.Flatten(); };

        private It should_return_the_inner_success = () => _result.ShouldEqual(_expectedResult);
    }
}