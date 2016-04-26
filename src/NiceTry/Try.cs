using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NiceTry {
    /// <summary>
    ///     Represents the success or failure of an operation. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("{ToString(),nq}")]
    public abstract class Try<T> : IComparable<Try<T>>, IEquatable<Try<T>>, IComparable, IStructuralComparable, IStructuralEquatable {

        /// <summary>
        ///     Indicates if this represents failure. 
        /// </summary>
        public bool IsFailure => Kind == TryKind.Failure;

        /// <summary>
        ///     Indicates if this represents success. 
        /// </summary>
        public bool IsSuccess => Kind == TryKind.Success;

        /// <summary>
        ///     Indicates the kind of this instance. 
        /// </summary>
        public abstract TryKind Kind { get; }

        public abstract void IfFailure([NotNull] Action<Exception> failure);

        public abstract void IfSuccess([NotNull] Action<T> success);

        public abstract void Match([NotNull] Action<T> success, [NotNull] Action<Exception> failure);

        public abstract B Match<B>([NotNull] Func<T, B> success, [NotNull] Func<Exception, B> failure);

        #region Equality

        public override bool Equals(object obj) =>
            ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);

        public bool Equals(Try<T> other) =>
            ((IStructuralEquatable)this).Equals(other, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
                    ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            if (ReferenceEquals(this, other)) return true;

            var @try = other as Try<T>;
            if (other.IsNull()) return false;

            return this.Match(
                failure: err => @try.Match(
                    failure: otherErr => comparer.Equals(err, otherErr),
                    success: _ => false),
                success: x => @try.Match(
                    failure: _ => false,
                    success: otherX => comparer.Equals(x, otherX)));
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => this.Match(
            failure: err => _.CombineHashCodes(comparer.GetHashCode(Kind), comparer.GetHashCode(err)),
            success: x => _.CombineHashCodes(comparer.GetHashCode(Kind), comparer.GetHashCode(x)));

        #endregion Equality

        #region Comparability

        public int CompareTo(Try<T> other) =>
            ((IStructuralComparable)this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (other.IsNull()) return 1;
            if (ReferenceEquals(this, other)) return 0;

            var @try = other as Try<T>;
            if (@try.IsNull())
                throw new ArgumentException("Provided object not of type Try<T>", nameof(other));

            return this.Match(
                failure: err => @try.Match(
                    failure: otherErr => comparer.Compare(err, otherErr),
                    success: _ => -1),
                success: x => @try.Match(
                    failure: _ => 1,
                    success: otherX => comparer.Compare(x, otherX)));
        }

        #endregion Comparability

        #region Formatting

        public override string ToString() => this.Match(
            failure: err => $"Failure({err.Message})",
            success: x => $"Success({x})");

        #endregion Formatting
    }
}