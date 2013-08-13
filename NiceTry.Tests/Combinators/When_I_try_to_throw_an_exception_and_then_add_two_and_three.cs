using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "AndThen")]
    internal class When_I_try_to_throw_an_exception_and_then_add_two_and_three {
        private static Func<ITry, ITry<int>> _addTwoAndThree;
        private static Func<ITry> _throwException;
        private static ITry<int> _result;

        private Establish context = () => {
            _addTwoAndThree = t => Try.To(() => 2 + 3);
            _throwException = () => { throw new Exception("Expected test exception"); };
        };

        private Because of = () => _result = Try.To(_throwException)
                                                .AndThen(t => _addTwoAndThree(t));

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}