using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (RetryExt), "Retry")]
    internal class When_I_try_to_add_two_and_three_and_add_one_up_to_two_times_which_succeeds_the_second_time
    {
        private static Try<int> _result;
        private static Func<int, int> _addOne;
        private static int _count;

        private Establish ctx = () => _addOne = i =>
        {
            _count += 1;

            if (_count < 2) throw new Exception("Expected test exception");

            return i + 1;
        };

        private Because of = () => _result = Try.To(() => 2 + 3)
            .Retry(_addOne, 2);

        private It should_contain_six_in_the_success =
            () => _result.Value.Should().Be(6);

        private It should_return_a_success =
            () => _result.IsSuccess.Should().BeTrue();
    }
}