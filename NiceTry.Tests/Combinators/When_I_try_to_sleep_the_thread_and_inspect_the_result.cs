using System;
using System.Threading;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Inspect")]
    internal class
        When_I_try_to_sleep_the_thread_and_inspect_the_result {
        static Action _sleep;
        static Action<ITry> _inspect;
        static ITry _inspectedResult;
        static ITry _result;

        Establish context = () => {
            _sleep = () => Thread.Sleep(50);
            _inspect = t => _inspectedResult = t;
        };

        Because of = () => _result = Try.To(_sleep)
                                        .Inspect(_inspect);

        It should_return_a_success =
            () => _result.IsSuccess.ShouldBeTrue();

        It should_return_the_same_success_from_the_inspection =
            () => _inspectedResult.ShouldEqual(_result);
    }
}