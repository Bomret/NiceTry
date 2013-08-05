using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_add_two_and_three_and_then_add_one {
        private static Func<int> _addTwoAndThree;
        private static Func<ITry<int>, ITry<int>> _addOne;
        private static ITry<int> _result;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _addOne = t => t.Map(i => i + 1);
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .AndThen(_addOne);

        private It should_contain_six_in_the_success = () => _result.Value.ShouldEqual(6);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}