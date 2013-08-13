using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (NiceTry.Extensions),"GetOrElse")]
    internal class When_I_try_to_divide_by_zero_and_return_zero_instead {
        private static Func<int> _divideByZero;
        private static int _result;
        private static int _zero;

        private Establish context = () => {
            _divideByZero = () => {
                var zero = 0;

                return 5 / zero;
            };

            _zero = 0;
        };

        private Because of = () => _result = Try.To(_divideByZero)
                                                .GetOrElse(_zero);

        private It should_return_zero = () => _result.ShouldEqual(_zero);
    }
}