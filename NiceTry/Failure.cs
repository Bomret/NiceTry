using System;

namespace NiceTry {
    public sealed class Failure<T> : ITry<T>,
                                     IEquatable<ITry<T>> {
        public Failure(Exception error) {
            Error = error;
        }

        public bool Equals(ITry<T> other) {
            return ReferenceEquals(other, this);
        }

        public bool IsSuccess {
            get { return false; }
        }

        public bool IsFailure {
            get { return true; }
        }

        public Exception Error { get; private set; }

        public T Value {
            get { throw new InvalidOperationException("A Failure does not contain a value"); }
        }

        public override string ToString() {
            return string.Format("Error: {0}", Error);
        }

        bool Equals(Failure<T> other) {
            return Equals(Error, other.Error);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Failure<T> && Equals((Failure<T>) obj);
        }

        public override int GetHashCode() {
            return (Error != null ? Error.GetHashCode() : 0);
        }

        public static implicit operator Exception(Failure<T> m) {
            return m.Error;
        }
    }
}