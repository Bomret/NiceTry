using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_divide_by_zero_and_return_zero_instead {
        static Func<int> _divideByZero;
        static int _result;
        static int _zero;

        Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _zero = 0;
        };

        Because of = () => _result = Try.To(_divideByZero)
                                        .GetOrElse(_zero);

        It should_return_zero = () => _result.ShouldEqual(_zero);
    }
}