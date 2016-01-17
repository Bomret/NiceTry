using System;
using System.Collections;
using JetBrains.Annotations;

namespace NiceTry {
    /// <summary>
    ///     Represents the success or failure of an operation that might have returned a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITry<T> : IComparable<ITry<T>>, IEquatable<ITry<T>>, IComparable, IStructuralComparable, IStructuralEquatable {
        /// <summary>
        ///     Returns a value that indicates if this represents failure.
        /// </summary>
        bool IsFailure { get; }

        /// <summary>
        ///     Returns a value that indicates if this represents success.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        ///     Executes one of the given side effects for success and failure, depending on wether this represents success or
        ///     failure.
        /// </summary>
        /// <param name="success">Side effect that is executed if this represents success.</param>
        /// <param name="failure">Side effect that is executed if this represents failure.</param>
        void Match([NotNull] Action<T> success, [NotNull] Action<Exception> failure);

        /// <summary>
        ///     Executes one of the given functions for success and failure and returns the result, depending on wether this
        ///     represents success or failure.
        /// </summary>
        /// <param name="success">Function that is executed if this represents success.</param>
        /// <param name="failure">Function that is executed if this represents failure.</param>
        [CanBeNull]
        R Match<R>([NotNull] Func<T, R> success, [NotNull] Func<Exception, R> failure);

        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> given the value if this represents success.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        void IfSuccess([NotNull] Action<T> sideEffect);

        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> if this represents failure.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        void IfFailure([NotNull] Action<Exception> sideEffect);
    }
}