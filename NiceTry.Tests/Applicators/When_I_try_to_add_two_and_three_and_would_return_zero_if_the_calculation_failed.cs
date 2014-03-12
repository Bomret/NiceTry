using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "GetOrElse")]
    class When_I_try_to_add_two_and_three_and_would_return_zero_if_the_calculation_failed {
        static int _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .GetOrElse(0);

        It should_return_five = () => _five.Should().Be(5);
    }
}