using System;

namespace NiceTry {
    public sealed class Success : ITry {
        public bool IsSuccess {
            get { return true; }
        }

        public bool IsFailure {
            get { return false; }
        }

        public Exception Error {
            get { throw new NotSupportedException("A Success does not contain an error"); }
        }
    }

    public sealed class Success<TValue> : ITry<TValue> {
        public Success(TValue value) {
            Value = value;
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

        public TValue Value { get; private set; }
    }
}