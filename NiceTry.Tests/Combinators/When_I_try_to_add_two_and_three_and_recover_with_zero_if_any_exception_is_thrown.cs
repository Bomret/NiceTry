using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Recover")]
    internal class When_I_try_to_add_two_and_three_and_recover_with_zero_if_any_exception_is_thrown
    {
        private static ITry<int> _result;
        private static Func<int> _addTwoAndThree;
        private static Func<Exception, int> _withZeroIfException;
        private static int _five;

        private Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _withZeroIfException = error => 0;
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .Recover(_withZeroIfException);

        private It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        private It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}