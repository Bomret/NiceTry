using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    internal class When_I_check_if_a_success_that_contains_two_does_contain_three
    {
        private static ITry<int> _twoSuccess;
        private static ITry<int> _result;
        private static Func<int, bool> _containsThree;

        private Establish context = () =>
        {
            _twoSuccess = new Success<int>(2);
            _containsThree = i => i == 3;
        };

        private Because of = () => _result = _twoSuccess.Filter(_containsThree);

        private It should_contain_an_ArgumentException_in_the_failure =
            () => _result.Error.ShouldBeOfType<ArgumentException>();

        private It should_return_a_failure_success = () => _result.IsFailure.ShouldBeTrue();
    }
}