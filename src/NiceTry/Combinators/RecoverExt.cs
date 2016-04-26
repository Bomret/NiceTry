using JetBrains.Annotations;
using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to recover from failure. 
    /// </summary>
    public static class RecoverExt {
        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an
        ///     exception, a <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T" />
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> Recover<T>([NotNull] this Try<T> @try, [NotNull] Func<Exception, T> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            return RecoverWith(@try, err => {
                var res = handleError(err);
                return Ok(res);
            });
        }

        /// <summary>
        ///     Tries to recover from failure using the specified <paramref name="handleError" /> if the specified
        ///     <paramref name="try" /> represents failure. If the specified <paramref name="handleError" /> throws an
        ///     exception, a <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="handleError" /> is <see langword="null" />.
        /// </exception>
        [NotNull]
        public static Try<T> RecoverWith<T>([NotNull] this Try<T> @try, [NotNull] Func<Exception, Try<T>> handleError) {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            return @try.Match(
                failure: err => Try(() => handleError(err)),
                success: _ => @try);
        }
    }
}