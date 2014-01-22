using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "WhenComplete")]
    class When_I_try_to_add_two_and_three_and_register_for_completion {
        static ITry<int> _five;

        Because of = () => Try.To(() => 2 + 3)
                              .WhenComplete(five => _five = five);

        It should_contain_the_expected_result_in_the_success = () => _five.Value.ShouldEqual(5);

        It should_not_return_a_failure = () => _five.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _five.IsSuccess.ShouldBeTrue();
    }
}