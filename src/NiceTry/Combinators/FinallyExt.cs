using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to execute side effects.
    /// </summary>
    public static class FinallyExt {
        /// <summary>
        ///     The specified <paramref name="action" /> is executed wether the specified <paramref name="try" /> represents
        ///     success or failure. If <paramref name="action" /> throws an exception, the resulting <see cref="Try{T}" /> will be
        ///     a <see cref="Failure{T}" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="action"></param>
        public static Try<T> Finally<T>(this Try<T> @try, Action action) {
            action.ThrowIfNull(nameof(action));
            @try.ThrowIfNull(nameof(@try));

            return Try(action).Match(
                failure: Fail<T>,
                success: _ => @try);
        }
    }
}