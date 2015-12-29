using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class RecoverExt {
        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry Recover(this ITry @try, Action<Exception> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: err => Try.To(() => handleError(err)),
                Success: Try.Success);
        }

        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T" />
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> Recover<T>(this ITry<T> @try, Func<Exception, T> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: err => Try.To(() => handleError(err)),
                Success: Try.Success);
        }

        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry RecoverWith(this ITry @try, Func<Exception, ITry> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: err => Try.To(() => handleError(err)),
                Success: Try.Success);
        }

        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an exception, a
        ///     <see cref="Failure" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static ITry<T> RecoverWith<T>(this ITry<T> @try, Func<Exception, ITry<T>> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                Failure: err => Try.To(() => handleError(err)),
                Success: Try.Success);
        }
    }
}