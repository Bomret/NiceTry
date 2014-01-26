using System;
using Machine.Specifications;

namespace NiceTry.Tests.Combinators {
    [Subject(typeof (NiceTry.Combinators), "Using")]
    class When_I_use_a_disposable_container_to_store_two {
        static ITry<Container<int>> _result;

        Because of = () => _result = Try.FromValue(2)
                                        .Using(() => new Container<int>(),
                                               (container, i) => container.StoreValue(i));

        It should_contain_a_disposed_container_in_the_result =
            () => _result.Value.IsDisposed.ShouldBeTrue();

        It should_contan_two_in_the_resulting_container =
            () => _result.Value.Value.ShouldEqual(2);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        class Container<T> : IDisposable {
            public bool IsDisposed { get; private set; }
            public T Value { get; private set; }

            public void Dispose() {
                IsDisposed = true;
            }

            public Container<T> StoreValue(T i) {
                Value = i;

                return this;
            }
        }
    }

    [Subject(typeof (NiceTry.Combinators), "UsingWith")]
    class When_I_use_a_disposable_container_to_store_two_and_return_a_try_containing_it {
        static ITry<Container<int>> _result;

        Because of = () => _result = Try.FromValue(2)
                                        .UsingWith(() => new Container<int>(),
                                                   (container, i) => container.TryStoreValue(i));

        It should_contain_a_disposed_container_in_the_result =
            () => _result.Value.IsDisposed.ShouldBeTrue();

        It should_contan_two_in_the_resulting_container =
            () => _result.Value.Value.ShouldEqual(2);

        It should_return_a_success = () => _result.IsSuccess.ShouldBeTrue();

        class Container<T> : IDisposable {
            public bool IsDisposed { get; private set; }
            public T Value { get; private set; }

            public void Dispose() {
                IsDisposed = true;
            }

            public ITry<Container<T>> TryStoreValue(T i) {
                Value = i;

                return Try.FromValue(this);
            }
        }
    }
}