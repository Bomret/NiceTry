using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

// ReSharper disable InconsistentNaming

namespace NiceTry {
    /// <summary>
    ///     Represents the successful outcome of an operation.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class Success : ITry {
        /// <summary>
        ///     Ths single instance of this object.
        /// </summary>
        internal static Success Instance { get; } = new Success();

        protected Success() {}

        public bool IsFailure => false;
        public bool IsSuccess => true;

        public void Match(Action success, Action<Exception> failure) =>
            success();

        public T Match<T>(Func<T> success, Func<Exception, T> failure) =>
            success();

        public void IfSuccess(Action sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            sideEffect();
        }

        public void IfFailure(Action<Exception> sideEffect) =>
            sideEffect.ThrowIfNull(nameof(sideEffect));

        #region Formatting

        public override string ToString() => "Success()";

        #endregion

        #region Comparability

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (other.IsNull()) return 1;

            var asTry = other as ITry;
            if (asTry.IsNull())
                throw new ArgumentException("Provided object not of type Try", nameof(other));

            return asTry.Match(
                failure: _ => 1,
                success: () => 0);
        }

        int IComparable<ITry>.CompareTo(ITry other) =>
            ((IStructuralComparable) this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable) this).CompareTo(obj, Comparer<object>.Default);

        #endregion

        #region Equality

        public bool Equals(ITry other) =>
            ((IStructuralEquatable) this).Equals(other, EqualityComparer<object>.Default);

        public override bool Equals(object obj) =>
            ((IStructuralEquatable) this).Equals(obj, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
            ((IStructuralEquatable) this).GetHashCode(EqualityComparer<object>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) =>
            other is Success;

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => 0;

        #endregion
    }

    /// <summary>
    ///     Represents the successful outcome of an operation that returned a result.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class Success<T> : Success, ITry<T> {
        readonly T _value;

        internal Success(T value) {
            _value = value;
        }

        public void Match(Action<T> success, Action<Exception> failure) => success(_value);
        public B Match<B>(Func<T, B> success, Func<Exception, B> failure) => success(_value);

        public void IfSuccess(Action<T> sideEffect) {
            sideEffect.ThrowIfNull(nameof(sideEffect));

            sideEffect(_value);
        }

        #region Formatting

        public override string ToString() => $"Success({_value})";

        #endregion

        #region Equality

        static int CombineHashCodes(int h1, int h2) =>
            ((h1 << 5) + h1) ^ h2;

        public bool Equals(ITry<T> other) =>
            ((IStructuralEquatable) this).Equals(other, EqualityComparer<object>.Default);

        public override bool Equals(object obj) =>
            ((IStructuralEquatable) this).Equals(obj, EqualityComparer<object>.Default);

        public override int GetHashCode() =>
            ((IStructuralEquatable) this).GetHashCode(EqualityComparer<object>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) {
            var success = other as Success<T>;
            if (success.IsNull()) return false;

            return IsSuccess == success.IsSuccess && comparer.Equals(_value, success._value);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            CombineHashCodes(comparer.GetHashCode(IsSuccess), comparer.GetHashCode(_value));

        #endregion

        #region Comparability

        int IStructuralComparable.CompareTo(object other, IComparer comparer) {
            if (other.IsNull()) return 1;

            var @try = other as ITry<T>;
            if (@try.IsNull())
                throw new ArgumentException("Provided object not of type ITry<T>", nameof(other));

            return @try.Match(
                failure: _ => 1,
                success: x => comparer.Compare(x, _value));
        }

        int IComparable<ITry<T>>.CompareTo(ITry<T> other) =>
            ((IStructuralComparable) this).CompareTo(other, Comparer<object>.Default);

        int IComparable.CompareTo(object obj) =>
            ((IStructuralComparable) this).CompareTo(obj, Comparer<object>.Default);

        #endregion
    }
}