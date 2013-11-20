using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "FlatMap")]
    internal class When_I_try_to_add_three_to_a_failure {
        static ITry<int> _failure;
        static Func<int, ITry<int>> _tryToAddThree;
        static ITry<int> _result;

        Establish context = () => {
            _failure = new Failure<int>(new ArgumentException());
            _tryToAddThree = i => Try.To(() => i + 3);
        };

        Because of = () => _result = _failure.FlatMap(_tryToAddThree);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}