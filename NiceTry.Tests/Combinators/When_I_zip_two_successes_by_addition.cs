using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (ZipExt), "Zip")]
    internal class When_I_zip_two_successes_by_addition
    {
        private static Try<int> _two;
        private static Try<int> _three;
        private static Try<int> _five;

        private Establish context = () =>
        {
            _two = Try.Success(2);
            _three = Try.Success(3);
        };

        private Because of = () => _five = _two.Zip(_three, (a, b) => a + b);

        private It should_contain_five_in_the_success = () => _five.Value.Should().Be(5);

        private It should_return_a_success = () => _five.IsSuccess.Should().BeTrue();
    }
}