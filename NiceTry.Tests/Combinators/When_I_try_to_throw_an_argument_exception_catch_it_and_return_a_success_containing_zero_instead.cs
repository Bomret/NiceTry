using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "CatchWith")]
    internal class
        When_I_try_to_throw_an_argument_exception_catch_it_and_return_a_success_containing_zero_instead {
        static Func<int> _throw;
        static ITry<int> _result;
        static Func<ArgumentException, ITry<int>> _catch;

        Establish context = () => {
            _throw = () => { throw new ArgumentException("Expected test exception"); };
            _catch = error => new Success<int>(0);
        };

        Because of = () => _result = Try.To(_throw)
                                        .CatchWith(_catch);

        It should_contain_zero_in_the_success =
            () => _result.Value.ShouldEqual(0);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();
    }
}