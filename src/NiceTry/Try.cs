using System;
using JetBrains.Annotations;

namespace NiceTry {
    /// <summary>
    ///     Represents the success or failure of an operation that might have returned a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITry<T> : ITry, IComparable<ITry<T>>, IEquatable<ITry<T>> {
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
    }
}