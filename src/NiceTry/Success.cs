using System;

namespace NiceTry {

    /// <summary> 
    ///     Represents the successful outcome of an operation. 
    /// </summary>
    public sealed class Success<T> : Try<T> {
        readonly T _value;

        internal Success(T value) : base (TryKind.Success) {
            _value = value;
        }

        public override void IfFailure(Action<Exception> sideEffect) =>
            sideEffect.ThrowIfNull (nameof (sideEffect));

        public override void IfSuccess(Action<T> sideEffect) {
            sideEffect.ThrowIfNull (nameof (sideEffect));

            sideEffect (_value);
        }

        public override void Match(Action<T> success, Action<Exception> failure) {
            success.ThrowIfNull (nameof (success));
            failure.ThrowIfNull (nameof (failure));

            success (_value);
        }

        public override B Match<B>(Func<T, B> success, Func<Exception, B> failure) {
            success.ThrowIfNull (nameof (success));
            failure.ThrowIfNull (nameof (failure));

            return success (_value);
        }
    }
}