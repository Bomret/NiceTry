using System;

namespace NiceTry {
    public sealed class Failure : ITry {
        public Failure(Exception error) {
            Error = error;
        }

        public bool IsSuccess {
            get { return false; }
        }

        public bool IsFailure {
            get { return true; }
        }

        public Exception Error { get; private set; }
    }

    public sealed class Failure<TValue> : ITry<TValue> {
        public Failure(Exception error) {
            Error = error;
        }

        public bool IsSuccess {
            get { return false; }
        }

        public bool IsFailure {
            get { return true; }
        }

        public Exception Error { get; private set; }

        public TValue Value {
            get { return default(TValue); }
        }
    }
}