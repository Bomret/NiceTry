using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators))]
    internal class When_I_check_if_a_success_that_contains_two_does_contain_two {
        static ITry<int> _twoSuccess;
        static ITry<int> _result;
        static Func<int, bool> _containsTwo;

        Establish context = () => {
            _twoSuccess = new Success<int>(2);
            _containsTwo = i => i == 2;
        };

        Because of = () => _result = _twoSuccess.Filter(_containsTwo);

        It should_return_the_original_success = () => _result.ShouldEqual(_twoSuccess);
    }
}