using System;
using JetBrains.Annotations;

namespace NiceTry {
    /// <summary>
    ///     Represents the failed outcome of an operation.
    /// </summary>
    public sealed class Failure<T> : Try<T> {
        public Exception Error { get; }

        internal Failure([NotNull] Exception error) {
            Error = error;
        }

        public override TryKind Kind => TryKind.Failure;
    }
}