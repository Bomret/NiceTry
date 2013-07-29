using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_check_if_a_failure_of_type_int_contains_two {
        private static ITry<int> _failure;
        private static ITry<int> _result;
        private static Func<int, bool> _containsTwo;

        private Establish context = () => {
            _failure = new Failure<int>(new ArgumentException());
            _containsTwo = i => i == 2;
        };

        private Because of = () => _result = _failure.Filter(_containsTwo);

        private It should_return_the_original_failure = () => _result.ShouldEqual(_failure);
    }
}