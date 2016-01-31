using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class RecoverExt {
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
        public static Try<T> Recover<T>(this Try<T> @try, Func<Exception, T> handleError) {
            handleError.ThrowIfNull(nameof(handleError));

            return RecoverWith(@try, err => {
                var res = handleError(err);
                return Try.Success(res);
            });
        }

        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> RecoverWith<T>(this Try<T> @try, Func<Exception, Try<T>> handleError) {
            @try.ThrowIfNullOrInvalid(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(
                failure: err => Try.To(() => handleError(err)),
                success: Try.Success);
        }
    }
}