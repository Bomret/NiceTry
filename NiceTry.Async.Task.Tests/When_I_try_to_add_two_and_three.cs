using System;
using Machine.Specifications;

namespace NiceTry.Async.Tests {
    [Subject(typeof (TryAsync), "To")]
    public class When_I_try_to_add_two_and_three {
        static ITry<int> _result;
        static Exception _error;

        Because of = () => _result = TryAsync.To(() => 2 + 3)
                                             .Synchronize();

        It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(5);

        It should_not_contain_an_exception =
            () => Catch.Exception(() => _result.Error).ShouldNotBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}