using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Catch")]
    internal class
        When_I_try_to_throw_a_generic_exception_and_would_catch_it_and_return_zero_if_it_was_an_argument_exception {
        static Func<int> _throw;
        static ITry<int> _result;
        static Func<ArgumentException, int> _catch;

        Establish context = () => {
            _throw = () => { throw new Exception("Expected test exception"); };
            _catch = error => 0;
        };

        Because of = () => _result = Try.To(_throw)
                                        .Catch(_catch);

        It should_return_a_failure =
            () => _result.IsFailure.ShouldBeTrue();
    }
}