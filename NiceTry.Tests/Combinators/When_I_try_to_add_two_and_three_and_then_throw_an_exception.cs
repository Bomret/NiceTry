using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Then")]
    internal class When_I_try_to_add_two_and_three_and_then_throw_an_exception {
        static Func<int> _addTwoAndThree;
        static Func<ITry<int>, ITry> _throwException;
        static ITry _result;

        Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _throwException = t => Try.To(() => {
                throw new Exception("Expected test exception");
            });
        };

        Because of = () => _result = Try.To(_addTwoAndThree)
            .Then(_throwException);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}