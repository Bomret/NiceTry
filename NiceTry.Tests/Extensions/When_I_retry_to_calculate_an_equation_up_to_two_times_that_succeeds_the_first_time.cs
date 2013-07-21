using System;
using Machine.Specifications;
using NiceTry.Extensions;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Retry))]
    internal class When_I_retry_to_calculate_an_equation_up_to_two_times_that_succeeds_the_first_time {
        static int _result;
        static int _expectedResult;
        static Func<int> _add;

        Establish context = () => {
            _add = () => 2 + 5;
            _expectedResult = _add();
        };

        Because of = () => _result = Retry.To(_add).Get();

        It should_return_the_expected_result = () => _result.ShouldEqual(_expectedResult);
    }
}