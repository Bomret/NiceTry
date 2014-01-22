using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "FlatMap")]
    class When_I_try_to_add_three_to_a_failure {
        static ITry<int> _failure;
        static ITry<int> _result;

        Establish context = () => { _failure = new Failure<int>(new Exception()); };

        Because of = () => _result = _failure.FlatMap(i => Try.To(() => i + 3));

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}