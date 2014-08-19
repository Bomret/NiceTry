using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (GetExt), "Get")]
    class When_I_try_to_add_two_and_three_and_get_the_result {
        static int _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .Get();

        It should_return_five = () => _five.Should().Be(5);
    }
}