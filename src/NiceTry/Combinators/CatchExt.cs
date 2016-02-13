using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to handle catched errors.
    /// </summary>
    public static class CatchExt {
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try"/> or <paramref name="handleError"/> is <see langword="null"/>.
        /// </exception>
        public static Try<T> Catch<TErr, T>(this Try<T> @try, Func<TErr, T> handleError)
            where TErr : Exception {
            handleError.ThrowIfNull(nameof(handleError));

            return CatchWith<TErr, T>(@try, err => {
                var res = handleError(err);
                return Ok(res);
            });
        }

        /// <summary>
        ///     If the specified <paramref name="try" /> represents failure and contains an exception of type
        ///     <typeparam name="TErr" />, the specified <paramref name="handleError" /> is executed and its result is returned.
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
        public static Try<T> CatchWith<TErr, T>(this Try<T> @try, Func<TErr, Try<T>> handleError)
            where TErr : Exception {
            handleError.ThrowIfNull(nameof(handleError));
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                success: _ => @try,
                failure: err => {
                    var asErr = err as TErr;
                    return asErr.IsNull() ? @try : Try(() => handleError(asErr));
                });
        }
    }
}