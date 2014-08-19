using System;
using System.Collections.Generic;
using System.Reactive;

namespace NiceTry {
    public abstract class Try<T> : IEquatable<Try<T>> {
        public abstract T Value { get; }
        public abstract bool IsSuccess { get; }
        public abstract bool IsFailure { get; }
        public abstract Exception Error { get; }

        public bool Equals(Try<T> other) {
            if (other == null) return false;

            if (other.IsSuccess && IsSuccess)
                return EqualityComparer<T>.Default.Equals(Value, other.Value);

            if (other.IsFailure && IsFailure)
                return EqualityComparer<Exception>.Default.Equals(Error, other.Error);

            return false;
        }

        public static implicit operator Try<T>(Failure failure) {
            return new Failure<T>(failure.Error);
        }
    }

    public static class Try {
        public static Try<T> To<T>(Func<T> work) {
            try {
                var result = work();
                return Success(result);
            }
            catch (Exception error) {
                return Failure(error);
            }
        }

        public static Try<Unit> To(Action work) {
            return To(() => {
                work();

                return Unit.Default;
            });
        }

        public static Failure Failure(Exception error) {
            return new Failure(error);
        }

        public static Try<T> Success<T>(T value) {
            return new Success<T>(value);
        }

        public static Try<T> Of<T>(T value) {
            return Success(value);
        }

        public static Try<T> Using<T, TDisposable>(Func<TDisposable> createDisposable,
                                                   Func<TDisposable, T> useDisposable) where TDisposable : IDisposable {
            return To(() => { using (var disposable = createDisposable()) return useDisposable(disposable); });
        }

        public static Try<T> UsingWith<T, TDisposable>(Func<TDisposable> createDisposable,
                                                       Func<TDisposable, Try<T>> useDisposable)
            where TDisposable : IDisposable {
            return To(() => { using (var disposable = createDisposable()) return useDisposable(disposable); })
                .Flatten();
        }
    }
}