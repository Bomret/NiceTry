using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (UsingWithExt), "UsingWith")]
    internal class When_I_use_a_disposable_container_to_store_two_and_return_a_try_containing_it
    {
        private static Try<Container<int>> _result;

        private Because of = () => _result = Try.Success(2)
            .UsingWith(() => new Container<int>(),
                (container, i) => container.TryStoreValue(i));

        private It should_contain_a_disposed_container_in_the_result =
            () => _result.Value.IsDisposed.Should().BeTrue();

        private It should_contan_two_in_the_resulting_container =
            () => _result.Value.Value.Should().Be(2);

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();

        private class Container<T> : IDisposable
        {
            public bool IsDisposed { get; private set; }
            public T Value { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }

            public Try<Container<T>> TryStoreValue(T i)
            {
                Value = i;

                return Try.Success(this);
            }
        }
    }
}