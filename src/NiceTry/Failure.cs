using System;

namespace NiceTry {
    /// <summary>
    ///     Represents the failed outcome of an operation.
    /// </summary>
    public sealed class Failure<T> : Try<T> {
        /// <summary>
        ///     The error that led to this failure.
        /// </summary>
        public Exception Error { get; }
        public override TryKind Kind => TryKind.Failure;

        internal Failure(Exception error) {
            Error = error;
        }
    }
}