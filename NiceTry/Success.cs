using System;
using System.Collections.Generic;

namespace NiceTry {
    public sealed class Success<T> : ITry<T>,
                                     IEquatable<ITry<T>> {
        public Success(T value) {
            Value = value;
        }

        public bool Equals(ITry<T> other) {
            return other.IsSuccess && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public bool IsSuccess {
            get { return true; }
        }

        public bool IsFailure {
            get { return false; }
        }

        public Exception Error {
            get { throw new InvalidOperationException("A Success does not contain an error"); }
        }

        public T Value { get; private set; }

        public override string ToString() {
            return string.Format("Value: {0}", Value);
        }

        public static implicit operator T(Success<T> success) {
            return success.Value;
        }

        public static implicit operator Success<T>(T value) {
            return new Success<T>(value);
        }
    }
}