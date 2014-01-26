using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Reject")]
    class When_I_reject_a_failure_if_it_contains_two {
        static ITry<int> _failure;
        static ITry<int> _result;

        Establish context = () => { _failure = new Failure<int>(new Exception()); };

        Because of = () => _result = _failure.Reject(i => i == 2);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}