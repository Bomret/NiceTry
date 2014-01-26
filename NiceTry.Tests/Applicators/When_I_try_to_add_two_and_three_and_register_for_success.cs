using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "WhenSuccess")]
    class When_I_try_to_add_two_and_three_and_register_for_success {
        static int _five;

        Because of = () => Try.To(() => 2 + 3)
                              .OnSuccess(five => _five = five);

        It should_return_five = () => _five.ShouldEqual(5);
    }
}