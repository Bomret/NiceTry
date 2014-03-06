using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "Success")]
    public class When_I_create_a_success_containing_three {
        static Try<int> _result;

        Because of = () => _result = Try.Success(3);

        It should_contain_three_in_the_success = () => _result.Value.ShouldEqual(3);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}