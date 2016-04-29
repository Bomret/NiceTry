using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NiceTry {

    /// <summary>
    /// Represents the success or failure of an operation. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("{ToString(),nq}")]
    public abstract class Try<T> : IComparable<Try<T>>, IEquatable<Try<T>>, IComparable, IStructuralComparable, IStructuralEquatable {
        protected bool _isSuccess;

        /// <summary>
        /// Indicates if this represents failure. 
        /// </summary>
        public bool IsFailure => !_isSuccess;

        /// <summary>
        /// Indicates if this represents success. 
        /// </summary>
        public bool IsSuccess => _isSuccess;

        /// <summary>
        /// Executes the specified <paramref name="sideEffect" /> only if this instance represents failure. 
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="sideEffect" /> is <see langword="null" />. </exception>
        public abstract void IfFailure([NotNull] Action<Exception> sideEffect);

        /// <summary>
        /// Executes the specified <paramref name="sideEffect" /> only if this instance represents success. 
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="sideEffect" /> is <see langword="null" />. </exception>
        public abstract void IfSuccess([NotNull] Action<T> success);

        /// <summary>
        /// Executes on of the specified side effects, depending on wether this instance represents
        /// success or failure.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
        /// </exception>
        public abstract void Match([NotNull] Action<T> success, [NotNull] Action<Exception> failure);

        /// <summary>
        /// Executes on of the specified side effects, depending on wether this instance represents
        /// success or failure, and returns the produced result.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="success" /> or <paramref name="failure" /> is <see langword="null" />.
        /// </exception>
        public abstract B Match<B>([NotNull] Func<T, B> success, [NotNull] Func<Exception, B> failure);

        #region Equality

        public override bool Equals(object obj) =>
            ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);

        public bool Equals(Try<T> other) =>
            ((IStructuralEquatable)this).Equals(other, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
                    ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            if (ReferenceEquals(this, other))
                return true;

            var @try = other as Try<T>;
            if (@try.IsNull())
                return false;

            return Match(
                failure: err => @try.Match(
                    failure: otherErr => comparer.Equals(err, otherErr),
                    success: _ => false),
                success: x => @try.Match(
                    failure: _ => false,
                    success: otherX => comparer.Equals(x, otherX)));
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => Match(
            failure: err => comparer.GetHashCode(err),
            success: x => comparer.GetHashCode(x));

        #endregion Equality

        #region Comparability

        public int CompareTo(Try<T> other) =>
            ((IStructuralComparable)this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (other.IsNull())
                return 1;
            if (ReferenceEquals(this, other))
                return 0;

            var @try = other as Try<T>;
            if (@try.IsNull())
                throw new ArgumentException("Provided object not of type Try<T>", nameof(other));

            return Match(
                failure: err => @try.Match(
                    failure: otherErr => comparer.Compare(err, otherErr),
                    success: _ => -1),
                success: x => @try.Match(
                    failure: _ => 1,
                    success: otherX => comparer.Compare(x, otherX)));
        }

        #endregion Comparability

        #region Formatting

        public override string ToString() => Match(
            failure: err => $"Failure({err.Message})",
            success: x => $"Success({x})");

        #endregion Formatting
    }
}