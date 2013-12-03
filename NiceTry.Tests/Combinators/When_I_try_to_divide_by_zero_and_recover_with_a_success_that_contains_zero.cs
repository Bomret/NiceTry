using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "RecoverWith")]
    internal class When_I_try_to_divide_by_zero_and_recover_with_a_success_that_contains_zero {
        static ITry<int> _result;
        static Func<int> _divideByZero;
        static Func<Exception, ITry<int>> _aSuccessThatContainsZero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _aSuccessThatContainsZero = error => new Success<int>(0);
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .RecoverWith(_aSuccessThatContainsZero);

        It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(0);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}