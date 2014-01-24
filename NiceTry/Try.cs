using System;
using System.Reactive;

namespace NiceTry {
    public static class Try {
        public static ITry<T> To<T>(Func<T> work) {
            try {
                var result = work();
                return new Success<T>(result);
            }
            catch (Exception error) {
                return new Failure<T>(error);
            }
        }

        public static ITry<Unit> To(Action work) {
            return To(() => {
                work();

                return Unit.Default;
            });
        }

        public static ITry<T> FromValue<T>(T value) {
            return new Success<T>(value);
        }

        public static ITry<T> Using<T, TDisposable>(Func<TDisposable> createDisposable,
                                                    Func<TDisposable, T> useDisposable) where TDisposable : IDisposable {
            return To(() => { using (var disposable = createDisposable()) return useDisposable(disposable); });
        }
    }
}