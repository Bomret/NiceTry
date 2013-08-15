using System;
using System.Globalization;
using Machine.Specifications;
using NiceTry.Async;

namespace NiceTry.Tests.Async.Combinators
{
    [Subject(typeof (NiceTry.Async.Combinators))]
    internal class When_I_try_to_add_two_and_three_and_convert_the_result_to_a_string
    {
        private static Func<int> _addTwoAndThree;
        private static string _result;
        private static Func<int, string> _toString;
        private static Action<ITry<string>> _setResult;

        private Establish context = () =>
        {
            _addTwoAndThree = () => 2 + 3;

            _toString = i => i.ToString(CultureInfo.InvariantCulture);
        };

        private Because of = () => _result = Try.ToAsync(_addTwoAndThree)
                                                .Map(_toString)
                                                .Get();

        private It should_return_five_as_a_string = () => _result.ShouldEqual("5");
    }
}