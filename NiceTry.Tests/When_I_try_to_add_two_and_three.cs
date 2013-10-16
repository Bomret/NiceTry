using System;
using Machine.Specifications;

namespace NiceTry.Tests
{
    [Subject(typeof (Try), "To")]
    public class When_I_try_to_add_two_and_three
    {
        static Func<int> _addTwoAndThree;
        static ITry<int> _result;
        static int _five;
        static Exception _error;

        Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        Because of = () => _result = Try.To(_addTwoAndThree);

        It should_contain_five_in_the_success = () => _result.Value.ShouldEqual(_five);

        It should_not_contain_an_exception = () => Catch.Exception(() => _error = _result.Error).ShouldNotBeNull();

        It should_not_return_a_failure = () => _result.IsFailure.ShouldBeFalse();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();
    }
}