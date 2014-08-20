using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_success_that_contain_the_same_value
    {
        private static Try<int> _success;

        private static bool _result;
        private static Try<int> _otherSuccess;

        private Establish context = () =>
        {
            _success = Try.Success(5);
            _otherSuccess = Try.Success(5);
        };

        private Because of = () => _result = _success.Equals(_otherSuccess);

        private It should_return_true = () => _result.Should().BeTrue();
    }
}