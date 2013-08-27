using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "FlatMap")]
    internal class When_I_try_to_add_three_to_a_success_that_contains_two
    {
        private static ITry<int> _twoSuccess;
        private static Func<int, ITry<int>> _addThree;
        private static ITry<int> _result;

        private Establish context = () =>
        {
            _twoSuccess = new Success<int>(2);
            _addThree = i => Try.To(() => i + 3);
        };

        private Because of = () => _result = _twoSuccess.FlatMap(_addThree);

        private It should_contain_five_in_the_Success = () => _result.Value.ShouldEqual(5);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}