using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_add_three_to_a_success_that_contains_two {
        static ITry<int> _twoSuccess;
        static Func<int, ITry<int>> _addThree;
        static ITry<int> _result;

        Establish context = () => {
            _twoSuccess = new Success<int>(2);
            _addThree = i => Try.To(() => i + 3);
        };

        Because of = () => _result = _twoSuccess.FlatMap(_addThree);

        It should_contain_five_in_the_Success = () => _result.Value.ShouldEqual(5);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}