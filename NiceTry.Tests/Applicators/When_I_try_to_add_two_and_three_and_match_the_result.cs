using System;
using Machine.Specifications;

namespace NiceTry.Tests.Applicators {
    [Subject(typeof (NiceTry.Applicators), "Match")]
    class When_I_try_to_add_two_and_three_and_match_the_result {
        static Exception _error;
        static int _five;

        Because of = () => Try.To(() => 2 + 3)
                              .Match(i => _five = i,
                                     e => _error = e);

        It should_execute_the_success_callback = () => _five.ShouldEqual(5);

        It should_not_execute_the_failure_callback = () => _error.ShouldBeNull();
    }
}