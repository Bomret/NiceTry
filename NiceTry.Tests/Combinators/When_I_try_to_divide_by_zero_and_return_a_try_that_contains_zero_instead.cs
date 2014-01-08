using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    class When_I_try_to_divide_by_zero_and_return_a_try_that_contains_zero_instead {
        static ITry<int> _result;

        Because of = () => _result = Try.To(() => 0)
                                        .Map(zero => 5 / zero)
                                        .OrElse(0);

        It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(0);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}