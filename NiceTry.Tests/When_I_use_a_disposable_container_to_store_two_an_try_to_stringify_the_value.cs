using System;
using Machine.Specifications;

namespace NiceTry.Tests {
    [Subject(typeof (Try), "UsingWith")]
    class When_I_use_a_disposable_container_to_store_two_an_try_to_stringify_the_value {
        static Try<string> _result;
        static Container<int> _container;

        Because of = () => _result = Try.UsingWith(() => new Container<int>(2),
                                                   container => {
                                                       _container = container;
                                                       return container.TryStringifyValue();
                                                   });

        It should_contain_two_as_string_in_the_result =
            () => _result.Value.ShouldEqual("2");

        It should_contain_two_in_the_disposed_container =
            () => _container.Value.ShouldEqual(2);

        It should_leave_a_disposed_container =
            () => _container.IsDisposed.ShouldBeTrue();

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        class Container<T> : IDisposable {
            public Container(T value) {
                Value = value;
            }

            public bool IsDisposed { get; private set; }
            public T Value { get; private set; }

            public void Dispose() {
                IsDisposed = true;
            }

            public Try<string> TryStringifyValue() {
                return Try.To(() => Value.ToString());
            }
        }
    }
}