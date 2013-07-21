using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_divide_by_zero_and_recover_with_zero_if_any_exception_is_thrown {
        static ITry<int> _result;
        static Func<int> _divideByZero;
        static Func<Exception, int> _withZero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _withZero = error => 0;
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .Recover(_withZero);

        It should_contain_zero_in_the_success = () => _result.Value.ShouldEqual(0);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}