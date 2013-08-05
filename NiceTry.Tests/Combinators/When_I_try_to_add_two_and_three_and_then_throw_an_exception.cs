using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_try_to_add_two_and_three_and_then_throw_an_exception {
        private static Func<int> _addTwoAndThree;
        private static Func<ITry<int>, ITry> _throwException;
        private static ITry _result;

        private Establish context = () => {
            _addTwoAndThree = () => 2 + 3;
            _throwException = t => Try.To(() => { throw new Exception("Expected test exception"); });
        };

        private Because of = () => _result = Try.To(_addTwoAndThree)
                                                .AndThen(_throwException);

        private It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}