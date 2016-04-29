using JetBrains.Annotations;
using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {

    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to execute side effects.
    /// </summary>
    public static class FinallyExt {

        /// <summary>
        ///     Executes the specified <paramref name="action" />, regardless if the specified
        ///     <paramref name="try" /> represents success or failure. If <paramref name="action" />
        ///     throws an exception, the resulting <see cref="Try{T}" /> will be a
        ///     <see cref="Failure{T}" />, otherwise the original one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="action"></param>
        [NotNull]
        public static Try<T> Finally<T>([NotNull] this Try<T> @try, [NotNull] Action action) {
            action.ThrowIfNull(nameof(action));
            @try.ThrowIfNull(nameof(@try));

            return Try(action).Match(
                failure: Fail<T>,
                success: _ => @try);
        }
    }
}