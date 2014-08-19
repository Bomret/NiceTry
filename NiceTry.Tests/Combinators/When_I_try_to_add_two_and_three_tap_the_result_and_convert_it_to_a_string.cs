using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (TapExt), "Tap")]
    class When_I_try_to_add_two_and_three_tap_the_result_and_convert_it_to_a_string {
        static Try<string> _result;
        static int _five;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Tap(i => _five = i)
                                        .Map(i => i.ToString());

        It should_return_five_as_a_string =
            () => _result.Value.Should().Be("5");

        It should_set_the_result_of_the_inspection_to_five_as_int =
            () => _five.Should().Be(5);
    }
}