using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Retry), "To")]
    class When_I_retry_to_add_two_and_three_up_to_two_times_which_succeeds_the_first_time {
        static Try<int> _result;

        Because of = () => _result = Retry.To(() => 2 + 3);

        It should_contain_five_in_the_success =
            () => _result.Value.ShouldEqual(5);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}