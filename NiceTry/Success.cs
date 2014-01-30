using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NiceTry {
    [DebuggerDisplay("Success(Value)")]
    sealed class Success<T> : Try<T> {
        readonly T _value;

        public Success(T value) {
            _value = value;
        }

        public override bool IsSuccess {
            get { return true; }
        }

        public override bool IsFailure {
            get { return false; }
        }

        public override Exception Error {
            get { throw new InvalidOperationException("A Success does not contain an error"); }
        }

        public override T Value {
            get { return _value; }
        }

        public override string ToString() {
            return string.Format("Success({0})", Value);
        }

        public override int GetHashCode() {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
}