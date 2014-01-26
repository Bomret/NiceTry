using Machine.Specifications;

namespace NiceTry.Async.Task.Tests {
    [Subject(typeof (TryAsync), "To")]
    public class When_I_try_to_divide_by_zero {
        static ITry<int> _result;

        Because of = () => _result = TryAsync.To(() => {
            var zero = 0;
            return 5 / zero;
        })
                                             .Synchronize();

        It should_contain_an_error = () => _result.Error.ShouldNotBeNull();

        It should_not_contain_a_value = () => Catch.Exception(() => _result.Value).ShouldNotBeNull();

        It should_not_return_a_success = () => _result.IsSuccess.ShouldBeFalse();

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}