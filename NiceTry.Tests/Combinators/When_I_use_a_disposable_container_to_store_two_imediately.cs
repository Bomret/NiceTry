using System;
using FluentAssertions;
using Machine.Specifications;
using NiceTry.Combinators;

namespace NiceTry.Tests.Combinators
{
    [Subject(typeof (UsingExt), "Using")]
    internal class When_I_use_a_disposable_container_to_store_two_imediately
    {
        private static Try<string> _result;
        private static Container<int> _container;

        private Because of = () => _result = Try.Success(2)
            .Using(i => new Container<int>(i),
                container =>
                {
                    _container = container;
                    return container.StringifyValue();
                });

        private It should_contain_two_as_string_in_the_result =
            () => _result.Value.Should().Be("2");

        private It should_contain_two_in_the_disposed_container =
            () => _container.Value.Should().Be(2);

        private It should_leave_a_disposed_container =
            () => _container.IsDisposed.Should().BeTrue();

        private It should_return_a_success = () => _result.IsSuccess.Should().BeTrue();

        private class Container<T> : IDisposable
        {
            public Container(T value)
            {
                Value = value;
            }

            public bool IsDisposed { get; private set; }
            public T Value { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }

            public string StringifyValue()
            {
                return Value.ToString();
            }
        }
    }
}