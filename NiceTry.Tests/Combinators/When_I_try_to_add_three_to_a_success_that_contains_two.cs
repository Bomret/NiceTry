using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (FlatMapExt), "FlatMap")]
    internal class When_I_try_to_add_three_to_a_success_that_contains_two
    {
        private static Try<int> _twoSuccess;
        private static Try<int> _result;

        private Establish context = () => { _twoSuccess = Try.Success(2); };

        private Because of = () => _result = _twoSuccess.FlatMap(i => Try.To(() => i + 3));

        private It should_contain_five_in_the_Success = () => _result.Value.Should().Be(5);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();
    }
}