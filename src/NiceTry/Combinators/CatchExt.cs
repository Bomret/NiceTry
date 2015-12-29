using System;
using JetBrains.Annotations;

namespace NiceTry.Combinators {
    public static class CatchExt {
        /// <summary>
        ///     If the specified <paramref name="try" /> represents failure and contains an exception of type
        ///     <typeparamref name="TErr" />, the specified <paramref name="handleError" /> is executed. If that fails a
        ///     <see cref="Failure" /> is returned, otherwise or if <paramref name="try" /> represents success,
        ///     <see cref="Success" /> is returned.
        /// </summary>
        /// <typeparam name="TErr"></typeparam>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="handleError"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static ITry Catch<TErr>([NotNull] this ITry @try, [NotNull] Action<TErr> handleError)
            where TErr : Exception {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(Try.Success, err => {
                var asErr = err as TErr;
                return asErr.IsNull() ? @try : Try.To(() => handleError(asErr));
            });
        }

        /// <summary>
        ///     If the specified <paramref name="try" /> represents failure and contains an exception of type
        ///     <typeparamref name="TErr" />, the specified <paramref name="handleError" /> is executed and its result is returned.
        ///     If that fails a <see cref="Failure{T}" /> is returned, otherwise or if <paramref name="try" /> represents success,
        ///     a <see cref="Success{T}" /> is returned.
        /// </summary>
        /// <typeparam name="TErr"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="handleError"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="handleError"/> is <see langword="null"/>.
        /// </exception>
        [NotNull]
        public static ITry<T> Catch<TErr, T>([NotNull] this ITry<T> @try, [NotNull] Func<TErr, T> handleError)
            where TErr : Exception {
            @try.ThrowIfNull(nameof(@try));
            handleError.ThrowIfNull(nameof(handleError));

            // ReSharper disable once AssignNullToNotNullAttribute
            return @try.Match(Try.Success, err => {
                var asErr = err as TErr;
                return asErr.IsNull() ? @try : Try.To(() => handleError(asErr));
            });
        }
    }
}