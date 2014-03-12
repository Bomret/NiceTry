using System;
using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    class When_I_retry_to_add_two_and_three_up_to_two_times_which_succeeds_the_second_time {
        static Try<int> _result;
        static int _five;
        static Func<int> _addTwoAndThreeButFailTheFirstTime;
        static int _try;

        Establish context = () => {
            _five = 2 + 3;

            _addTwoAndThreeButFailTheFirstTime = () => {
                _try += 1;

                if (_try < 2)
                    throw new ArgumentException("Expected test exception.");

                return _five;
            };
        };

        Because of = () => _result = Retry.To(_addTwoAndThreeButFailTheFirstTime);

        It should_contain_five_in_the_success =
            () => _result.Value.Should().Be(_five);

        It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}