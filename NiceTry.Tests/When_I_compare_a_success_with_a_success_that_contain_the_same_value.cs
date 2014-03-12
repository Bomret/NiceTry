using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Equals")]
    public class When_I_compare_a_success_with_a_success_that_contain_the_same_value {
        static Try<int> _success;

        static bool _result;
        static Try<int> _otherSuccess;

        Establish context = () => {
            _success = Try.Success(5);
            _otherSuccess = Try.Success(5);
        };

        Because of = () => _result = _success.Equals(_otherSuccess);

        It should_return_true = () => _result.Should().BeTrue();
    }
}