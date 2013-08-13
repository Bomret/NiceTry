using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    internal class When_I_check_if_a_success_that_contains_two_does_contain_two {
        private static ITry<int> _twoSuccess;
        private static ITry<int> _result;
        private static Func<int, bool> _containsTwo;

        private Establish context = () => {
            _twoSuccess = new Success<int>(2);
            _containsTwo = i => i == 2;
        };

        private Because of = () => _result = _twoSuccess.Filter(_containsTwo);

        private It should_return_the_original_success = () => _result.ShouldEqual(_twoSuccess);
    }
}