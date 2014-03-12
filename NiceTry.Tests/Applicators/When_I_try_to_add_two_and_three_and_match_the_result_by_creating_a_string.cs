using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Match")]
    class When_I_try_to_add_two_and_three_and_match_the_result_by_creating_a_string {
        static string _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Match(i => i.ToString(),
                                               e => "0");

        It should_return_five_as_string = () => _result.Should().Be("5");
    }
}