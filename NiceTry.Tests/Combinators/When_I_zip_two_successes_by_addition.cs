using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Zip")]
    class When_I_zip_two_successes_by_addition {
        static Try<int> _two;
        static Try<int> _three;
        static Try<int> _five;

        Establish context = () => {
            _two = Try.Success(2);
            _three = Try.Success(3);
        };

        Because of = () => _five = _two.Zip(_three, (a, b) => a + b);

        It should_contain_five_in_the_success = () => _five.Value.Should().Be(5);

        It should_return_a_success = () => _five.IsSuccess.Should().BeTrue();
    }
}