using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Zip")]
    class When_I_zip_two_successes_by_addition {
        static Success<int> _two;
        static Success<int> _three;
        static ITry<int> _five;

        Establish context = () => {
            _two = new Success<int>(2);
            _three = new Success<int>(3);
        };

        Because of = () => _five = _two.Zip(_three, (a, b) => a + b);

        It should_contain_five_in_the_success = () => _five.Value.ShouldEqual(5);

        It should_return_a_success = () => _five.IsSuccess.ShouldBeTrue();
    }
}