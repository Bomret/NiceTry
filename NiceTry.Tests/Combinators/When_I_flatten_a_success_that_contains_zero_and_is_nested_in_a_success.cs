using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_that_contains_zero_and_is_nested_in_a_success
    {
        static Success<Success<int>> _nestedSuccess;
        static ITry<int> _result;
        static Success<int> _expectedResult;

        Establish context = () =>
        {
            _expectedResult = new Success<int>(0);
            _nestedSuccess = new Success<Success<int>>(_expectedResult);
        };

        Because of = () => { _result = _nestedSuccess.Flatten(); };

        It should_return_the_inner_success = () => _result.ShouldEqual(_expectedResult);
    }
}