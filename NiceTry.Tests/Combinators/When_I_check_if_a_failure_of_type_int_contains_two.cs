using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    internal class When_I_check_if_a_failure_of_type_int_contains_two {
        static ITry<int> _failure;
        static ITry<int> _result;
        static Func<int, bool> _containsTwo;

        Establish context = () => {
            _failure = new Failure<int>(new ArgumentException());
            _containsTwo = i => i == 2;
        };

        Because of = () => _result = _failure.Filter(_containsTwo);

        It should_return_a_failure = () => _result.IsFailure.ShouldBeTrue();
    }
}