using System;

namespace NiceTry.Combinators {
    public static class FinallyExt {
        /// <summary>
        ///     The specified <paramref name="action" /> is executed wether the specified <paramref name="try" /> represents
        ///     success or failure. If <paramref name="action" /> throws an exception, the resulting <see cref="ITry{T}" /> will be
        ///     a <see cref="Failure{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Try<T> Finally<T>(this Try<T> @try, Action action) {
            action.ThrowIfNull(nameof(action));
            @try.ThrowIfNullOrInvalid(nameof(@try));

            return Try.To(action).Match(
                failure: Try.Failure<T>,
                success: _ => @try);
        }
    }
}