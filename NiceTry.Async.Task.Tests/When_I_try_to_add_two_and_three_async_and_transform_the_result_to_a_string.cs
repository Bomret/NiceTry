using System;
using System.Globalization;
using Machine.Specifications;

namespace NiceTry.Async.Tests {
    [Subject(typeof (AsyncCombinators), "Map")]
    public class When_I_try_to_add_two_and_three_async_and_transform_the_result_to_a_string {
        static ITry<string> _result;
        static Exception _error;

        Because of = () => _result = TryAsync.To(() => 2 + 3)
                                             .Map(i => i.ToString(CultureInfo.InvariantCulture))
                                             .Synchronize();

        It should_contain_five_as_string_in_the_success = () => _result.Value.ShouldEqual("5");

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}