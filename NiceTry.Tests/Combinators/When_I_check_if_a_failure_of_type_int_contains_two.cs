using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Filter")]
    class When_I_check_if_a_failure_of_type_int_contains_two {
        static Try<int> _failure;
        static Try<int> _result;

        Establish context = () => { _failure = Try.Failure(new Exception()); };

        Because of = () => _result = _failure.Filter(i => i == 2);

        It should_return_a_failure = () => 
            _result.IsFailure.ShouldBeTrue();
    }
}