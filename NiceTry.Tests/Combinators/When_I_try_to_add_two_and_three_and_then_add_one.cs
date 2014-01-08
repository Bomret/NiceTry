using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "AndThen")]
    class When_I_try_to_add_two_and_three_and_then_add_one {
        static Func<int> _addTwoAndThree;
        static Func<ITry<int>, ITry<int>> _addOne;
        static ITry<int> _result;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _addOne = t => t.Map(i => i + 1);
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Then(_addOne);

        It should_contain_six_in_the_success = () => _result.Value.ShouldEqual(6);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}