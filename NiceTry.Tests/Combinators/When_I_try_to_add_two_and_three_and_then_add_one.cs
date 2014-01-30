using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "ThenWith")]
    class When_I_try_to_add_two_and_three_and_then_add_one {
        static Try<int> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .ThenWith(t => t.Map(i => i + 1));

        It should_contain_six_in_the_success = () => _result.Value.ShouldEqual(6);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}