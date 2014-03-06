using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Retry")]
    class When_I_try_to_add_two_and_three_and_add_one_up_to_two_times_which_would_succeed_the_fourth_time {
        static Try<int> _result;
        static Func<int, int> _addOne;
        static int _count;

        Establish ctx = () => _addOne = i => {
            _count += 1;
            if (_count < 4) throw new Exception("Expected test exception");

            return i + 1;
        };

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Retry(_addOne, 2);

        It should_return_a_failure =
            () => _result.IsFailure.ShouldBeTrue();
    }
}