using System;
using System.Diagnostics;

namespace NiceTry {
    public sealed class Failure {
        public Failure(Exception error) {
            Error = error;
        }

        public Exception Error { get; private set; }

        public override string ToString() {
            return string.Format("Failure({0})", Error.Message);
        }

        public override int GetHashCode() {
            return Error.GetHashCode();
        }
    }

    sealed class Failure<T> : Try<T> {
        readonly Exception _error;

        public Failure(Exception error) {
            _error = error;
        }

        public override bool IsSuccess {
            get { return false; }
        }

        public override bool IsFailure {
            get { return true; }
        }

        public override Exception Error {
            get { return _error; }
        }

        public override T Value {
            get { throw new InvalidOperationException("A Failure does not contain a value"); }
        }

        public override string ToString() {
            return string.Format("Failure({0})", Error.Message);
        }

        public override int GetHashCode() {
            return Error.GetHashCode();
        }
    }
}