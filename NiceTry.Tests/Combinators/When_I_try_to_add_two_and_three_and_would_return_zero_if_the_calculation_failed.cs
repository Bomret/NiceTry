using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (GetExt), "GetOrElse")]
    class When_I_try_to_add_two_and_three_and_would_return_zero_if_the_calculation_failed {
        static int _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .GetOrElse(0);

        It should_return_five = () => _five.Should().Be(5);
    }
}