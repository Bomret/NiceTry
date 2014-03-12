using FluentAssertions;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Transform")]
    class When_I_try_to_add_two_and_three_and_transform_the_result_to_a_try_containing_a_string {
        static Try<string> _result;

        Because of = () => _result = Try.To(() => 2 + 3)
                                        .Transform(i => i.ToString(),
                                                   e => e.Message);

        It should_contain_five_as_a_string_in_the_success =
            () => _result.Value.Should().Be("5");

        It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}