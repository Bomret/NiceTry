using System;

namespace NiceTry {
    /// <summary>
    ///     Represents the successful outcome of an operation. 
    /// </summary>
    public sealed class Success<T> : Try<T> {
        T _value;

        internal Success(T value) {
            _value = value;
            _isSuccess = true;
        }

        public override void IfFailure(Action<Exception> failure) {
            failure.ThrowIfNull(nameof(failure));
        }

        public override void IfSuccess(Action<T> success) {
            success.ThrowIfNull(nameof(success));

            success(_value);
        }

        public override void Match(Action<T> success, Action<Exception> failure) {
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            success(_value);
        }

        public override B Match<B>(Func<T, B> success, Func<Exception, B> failure) {
            success.ThrowIfNull(nameof(success));
            failure.ThrowIfNull(nameof(failure));

            return success(_value);
        }
    }
}