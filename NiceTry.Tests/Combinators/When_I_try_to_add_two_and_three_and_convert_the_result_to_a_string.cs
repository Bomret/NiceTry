using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Map")]
    class When_I_try_to_add_two_and_three_and_convert_the_result_to_a_string {
        static Try<string> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Map(i => i.ToString());

        It should_return_five_as_a_string = () => _result.Value.ShouldEqual("5");
    }
}