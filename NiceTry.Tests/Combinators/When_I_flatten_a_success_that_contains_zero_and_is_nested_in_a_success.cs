using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FlattenExt), "Flatten")]
    public class When_I_flatten_a_success_that_contains_zero_and_is_nested_in_a_success
    {
        private static Try<Try<int>> _nestedSuccess;
        private static Try<int> _result;
        private static Try<int> _expectedResult;

        private Establish context = () =>
        {
            _expectedResult = Try.Success(0);
            _nestedSuccess = Try.Success(_expectedResult);
        };

        private Because of = () => { _result = _nestedSuccess.Flatten(); };

        private It should_return_the_inner_success = () => _result.Should().Be(_expectedResult);
    }
}