using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Inspect")]
    internal class
        When_I_try_to_throw_an_exception_and_inspect_the_result {
        static Action _throw;
        static Action<ITry> _inspect;
        static ITry _inspectedResult;
        static ITry _result;

        Establish context = () => {
            _throw = () => { throw new Exception("Expected test exception"); };
            _inspect = t => _inspectedResult = t;
        };

        Because of = () => _result = Try.To(_throw)
                                        .Inspect(_inspect);

        It should_return_a_failure =
            () => _result.IsFailure.ShouldBeTrue();

        It should_return_the_same_failure_from_the_inspection =
            () => _inspectedResult.ShouldEqual(_result);
    }
}