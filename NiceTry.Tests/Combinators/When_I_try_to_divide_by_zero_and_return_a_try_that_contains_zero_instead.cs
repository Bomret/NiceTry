using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "OrElse")]
    class When_I_try_to_divide_by_zero_and_return_a_try_that_contains_zero_instead {
        static Func<int> _divideByZero;
        static ITry<int> _result;
        static ITry<int> _zero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _zero = new Success<int>(0);
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .OrElse(_zero);

        It should_contaion_zero_in_the_success = () => _result.Value.ShouldEqual(_zero.Value);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}