using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_success_that_contain_different_values
    {
        private static Try<int> _success;

        private static bool _result;
        private static Try<int> _otherSuccess;

        private Establish context = () =>
        {
            _success = Try.Success(5);
            _otherSuccess = Try.Success(0);
        };

        private Because of = () => _result = _success.Equals(_otherSuccess);

        private It should_return_false = () => _result.Should().BeFalse();
    }
}