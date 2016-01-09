using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace NiceTry {
    /// <summary>
    ///     Represents the failure of an operation.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class Failure : ITry {
        protected readonly Exception _error;

        internal Failure([NotNull] Exception error) {
            _error = error;
        }

        public bool IsFailure => true;
        public bool IsSuccess => false;

        public void Match(Action success, Action<Exception> failure) =>
            failure(_error);

        public T Match<T>(Func<T> success, Func<Exception, T> failure) =>
            failure(_error);

        public void IfSuccess(Action sideEffect) =>
            sideEffect.ThrowIfNull(nameof(sideEffect));

        public void IfFailure(Action<Exception> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            sideEffect(_error);
        }

        #region Formatting

        public override string ToString() {
            return $"Failure({_error.GetType().Name})";
        }

        #endregion

        #region Equality

        static int CombineHashCodes(int h1, int h2) =>
            ((h1 << 5) + h1) ^ h2;

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            var failure = other as Failure;
            return !failure.IsNull() && comparer.Equals(_error, failure._error);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            CombineHashCodes(comparer.GetHashCode(IsFailure), comparer.GetHashCode(_error));

        public bool Equals(ITry other) =>
            ((IStructuralEquatable) this).Equals(other, EqualityComparer<object>.Default);

        public override bool Equals(object obj) =>
            ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
            ((IStructuralEquatable) this).GetHashCode(EqualityComparer<object>.Default);

        #endregion

        #region Comparability

        int IComparable<ITry>.CompareTo(ITry other) =>
            ((IStructuralComparable) this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (other.IsNull()) return 1;

            var @try = other as ITry;
            if (@try.IsNull())
                throw new ArgumentException("Provided object not of type Try", nameof(other));

            return @try.Match(
                failure: err => comparer.Compare(err, _error),
                success: () => -1);
        }

        #endregion
    }

    /// <summary>
    ///     Represents the successful outcome of an operation that might have returned a result.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class Failure<T> : Failure, ITry<T> {
        public Failure([NotNull] Exception error) : base(error) {}

        public void Match(Action<T> success, Action<Exception> failure) =>
            failure(_error);

        public B Match<B>(Func<T, B> success, Func<Exception, B> failure) =>
            failure(_error);

        public void IfSuccess(Action<T> sideEffect) =>
            sideEffect.ThrowIfNull(nameof(sideEffect));

        #region Equality

        static int CombineHashCodes(int h1, int h2) =>
            ((h1 << 5) + h1) ^ h2;

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            var failure = other as Failure<T>;
            return !failure.IsNull() && comparer.Equals(_error, failure._error);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            CombineHashCodes(comparer.GetHashCode(IsFailure), comparer.GetHashCode(_error));

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
            if (other.IsNull()) return 1;

            var @try = other as ITry<T>;
            if (@try.IsNull())
                throw new ArgumentException("Provided object not of type ITry<T>", nameof(other));

            return @try.Match(
                failure: err => comparer.Compare(err, _error),
                success: () => -1);
        }

        #endregion
    }
}