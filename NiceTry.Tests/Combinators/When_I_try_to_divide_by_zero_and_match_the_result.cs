using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (MatchExt), "Match")]
    class When_I_try_to_divide_by_zero_and_match_the_result {
        static Func<int> _divideByZero;
        static bool _successCallbackExecuted;
        static Exception _error;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };
        };

        Because of = () => Try.To(_divideByZero)
                              .Match(i => _successCallbackExecuted = true,
                                     error => _error = error);

        It should_execute_the_failure_callback = () => _error.Should().NotBeNull();

        It should_not_execute_the_success_callback = () => _successCallbackExecuted.Should().BeFalse();
    }
}