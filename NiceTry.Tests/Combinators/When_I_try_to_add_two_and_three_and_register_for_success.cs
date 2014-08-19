using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (OnSuccessExt), "OnSuccess")]
    class When_I_try_to_add_two_and_three_and_register_for_success {
        static int _five;

        Because of = () => Try.To(() => 2 + 3)
                              .OnSuccess(five => _five = five);

        It should_return_five = () => _five.Should().Be(5);
    }
}