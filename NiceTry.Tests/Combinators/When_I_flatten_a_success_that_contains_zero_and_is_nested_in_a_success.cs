using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Flatten")]
    public class When_I_flatten_a_success_that_contains_zero_and_is_nested_in_a_success {
        static Try<Try<int>> _nestedSuccess;
        static Try<int> _result;
        static Try<int> _expectedResult;

        Establish context = () => {
            _expectedResult = Try.Success(0);
            _nestedSuccess = Try.Success(_expectedResult);
        };

        Because of = () => { _result = _nestedSuccess.Flatten(); };

        It should_return_the_inner_success = () => _result.Should().Be(_expectedResult);
    }
}