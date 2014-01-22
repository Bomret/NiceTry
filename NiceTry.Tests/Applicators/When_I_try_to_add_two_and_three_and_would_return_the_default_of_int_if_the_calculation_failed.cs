using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "GetOrDefault")]
    class When_I_try_to_add_two_and_three_and_would_return_the_default_of_int_if_the_calculation_failed {
        static int _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .GetOrDefault();

        It should_return_five = () => _five.ShouldEqual(5);
    }
}