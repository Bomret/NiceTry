using System;
using System.Collections;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace NiceTry {
    /// <summary>
    ///     Represents the success or failure of an operation.
    /// </summary>
    public interface ITry : IStructuralEquatable, IStructuralComparable, IComparable<ITry>, IComparable,
        IEquatable<ITry> {
        /// <summary>
        ///     Returns a value that indicates if this represents failure.
        /// </summary>
        bool IsFailure { get; }

        /// <summary>
        ///     Returns a value that indicates if this represents success.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        ///     Executes one of the given side effects for success and failure, depending on wether this represents success
        ///     or failure.
        /// </summary>
        /// <param name="Success">Side effect that is executed if this represents success.</param>
        /// <param name="Failure">Side effect that is executed if this represents failure.</param>
        void Match([NotNull] Action Success, [NotNull] Action<Exception> Failure);

        /// <summary>
        ///     Executes one of the given functions for success and failure and returns the result, depending on wether this Try
        ///     represents a success or a failure.
        /// </summary>
        /// <param name="Success">Function that is executed if this represents success.</param>
        /// <param name="Failure">Function that is executed if this represents failure.</param>
        [CanBeNull]
        T Match<T>([NotNull] Func<T> Success, [NotNull] Func<Exception, T> Failure);

        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> if this represents success.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        void IfSuccess([NotNull] Action sideEffect);

        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> if this represents failure.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        void IfFailure([NotNull] Action<Exception> sideEffect);
    }

    /// <summary>
    ///     Represents the success or failure of an operation that might have returned a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITry<T> : ITry, IComparable<ITry<T>>, IEquatable<ITry<T>> {
        /// <summary>
        ///     Executes one of the given side effects for success and failure, depending on wether this represents success or
        ///     failure.
        /// </summary>
        /// <param name="Success">Side effect that is executed if this represents success.</param>
        /// <param name="Failure">Side effect that is executed if this represents failure.</param>
        void Match([NotNull] Action<T> Success, [NotNull] Action<Exception> Failure);

        /// <summary>
        ///     Executes one of the given functions for success and failure and returns the result, depending on wether this
        ///     represents success or failure.
        /// </summary>
        /// <param name="Success">Function that is executed if this represents success.</param>
        /// <param name="Failure">Function that is executed if this represents failure.</param>
        [CanBeNull]
        R Match<R>([NotNull] Func<T, R> Success, [NotNull] Func<Exception, R> Failure);

        /// <summary>
        ///     Executes the specified <paramref name="sideEffect" /> given the value if this represents success.
        /// </summary>
        /// <param name="sideEffect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sideEffect" /> is <see langword="null" />.
        /// </exception>
        void IfSuccess([NotNull] Action<T> sideEffect);
    }
}