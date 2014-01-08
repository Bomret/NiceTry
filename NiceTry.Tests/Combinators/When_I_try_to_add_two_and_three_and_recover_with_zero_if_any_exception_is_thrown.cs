using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Recover")]
    class When_I_try_to_add_two_and_three_and_recover_with_zero_if_any_exception_is_thrown {
        static ITry<int> _result;
        static Func<int> _addTwoAndThree;
        static Func<Exception, int> _withZeroIfException;
        static int _five;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();

            _withZeroIfException = error => 0;
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Recover(_withZeroIfException);

        It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}