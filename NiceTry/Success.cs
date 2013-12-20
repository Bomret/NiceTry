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
            get { throw new NotSupportedException("A Success does not contain an error"); }
        }

        public T Value { get; private set; }
    }
}