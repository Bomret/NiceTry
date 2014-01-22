using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Recover")]
    class When_I_try_to_add_two_and_three_and_recover_with_zero_if_any_exception_is_thrown {
        static ITry<int> _five;

        Because of = () => _five = Try.To(() => 2 + 3)
                                      .Recover(e => 0);

        It should_contain_five_in_the_success = () => _five.Value.ShouldEqual(5);

        It should_return_a_success = () => _five.IsSuccess.ShouldBeTrue();
    }
}