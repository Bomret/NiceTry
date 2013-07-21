using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_get_the_result {
        static Func<int> _calculateEquation;
        static int _result;
        static int _expectedResult;

        Establish context = () => {
            _calculateEquation = () => 2 + 3;
            _expectedResult = _calculateEquation();
        };

        Because of = () => _result = Try.To(_calculateEquation)
                                        .Get();

        It should_return_the_expected_result = () => _result.ShouldEqual(_expectedResult);
    }
}