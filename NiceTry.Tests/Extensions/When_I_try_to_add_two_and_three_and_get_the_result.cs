using System;
using Machine.Specifications;

namespace NiceTry.Tests.Extensions {
    [Subject(typeof (Applicators), "Get")]
    internal class When_I_try_to_add_two_and_three_and_get_the_result {
        static Func<int> _addTwoAndThree;
        static int _result;
        static int _five;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _five = _addTwoAndThree();
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
                                        .Get();

        It should_return_five = () => _result.ShouldEqual(_five);
    }
}