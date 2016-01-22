using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace NiceTry {
    /// <summary>
    ///     Represents the failed outcome of an operation.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class Failure<T> : ITry<T> {
        readonly Exception _error;

        public bool IsFailure => true;
        public bool IsSuccess => false;

        internal Failure([NotNull] Exception error) {
            _error = error;
        }

        public void Match(Action<T> success, Action<Exception> failure) =>
            failure(_error);

        public B Match<B>(Func<T, B> success, Func<Exception, B> failure) =>
            failure(_error);

        public void IfSuccess(Action<T> sideEffect) =>
            sideEffect.ThrowIfNull(nameof(sideEffect));

        public void IfFailure(Action<Exception> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            sideEffect(_error);
        }

        #region Equality

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            var failure = other as Failure<T>;

            return failure.IsNotNull() && comparer.Equals(_error, failure._error);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            _.CombineHashCodes(comparer.GetHashCode(IsFailure), comparer.GetHashCode(_error));

        public bool Equals(ITry<T> other) =>
            ((IStructuralEquatable)this).Equals(other, EqualityComparer<object>.Default);

        public override bool Equals(object obj) =>
            ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
            ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);

        #endregion

        #region Comparability

        int IComparable<ITry<T>>.CompareTo(ITry<T> other) =>
            ((IStructuralComparable)this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if(other.IsNull()) return 1;
            if(ReferenceEquals(this, other)) return 0;

            var @try = other as ITry<T>;
            if(@try.IsNull())
                throw new ArgumentException("Provided object not of type ITry<T>", nameof(other));

            return @try.Match(
                failure: err => comparer.Compare(err, _error),
                success: _ => -1);
        }

        #endregion
    }
}