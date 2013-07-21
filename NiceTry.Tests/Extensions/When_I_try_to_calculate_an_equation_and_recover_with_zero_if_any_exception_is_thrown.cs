using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (TryExtensions))]
    internal class When_I_try_to_calculate_an_equation_and_recover_with_zero_if_any_exception_is_thrown {
        static ITry<int> _result;
        static Func<int> _add;
        static Func<Exception, int> _withZeroIfException;
        static int _expectedResult;

        Establish context = () => {
            _add = () => 2 + 5;
            _expectedResult = _add();

            _withZeroIfException = error => 0;
        };

        Because of = () => _result = Try.To(_add)
                                        .Recover(_withZeroIfException);

        It should_contain_the_expected_result_in_the_success = () => _result.Value.ShouldEqual(_expectedResult);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}