using System;

namespace NiceTry {
    /// <summary>
    ///     Represents the failed outcome of an operation. 
    /// </summary>
    public sealed class Failure<T> : Try<T> {
        Exception _error;

        internal Failure(Exception error) {
            _error = error;
            _isSuccess = false;
        }

        public override void IfFailure(Action<Exception> failure) {
            failure.ThrowIfNull(nameof(failure));

            failure(_error);
        }

        public override void IfSuccess(Action<T> success) {
            success.ThrowIfNull(nameof(success));
        }

        public override void Match(Action<T> success, Action<Exception> failure) {
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            failure(_error);
        }

        public override B Match<B>(Func<T, B> success, Func<Exception, B> failure) {
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return failure(_error);
        }
    }
}