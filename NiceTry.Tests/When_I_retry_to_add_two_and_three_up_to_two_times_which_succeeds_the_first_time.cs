using System;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Retry), "To")]
    class When_I_retry_to_add_two_and_three_up_to_two_times_which_succeeds_the_first_time
    {
        static ITry<int> _result;
        static int _five;
        static Func<int> _addTwoAndThree;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        Because of = () => _result = Retry.To(_addTwoAndThree);

        It should_contain_five_in_the_success =
            () => _result.Value.ShouldEqual(_five);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}