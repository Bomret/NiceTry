using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to execute side effects on the value therein.
    /// </summary>
    public static class DoExt {
        /// <summary>
        ///     Executes the specified <paramref name="action" /> on the contained value if the specified <paramref name="try" />
        ///     represents success. If it represents failure or the <paramref name="action" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <param name="try"></param>
        /// <param name="action"></param>
        /// <returns>A success if the operation succeeded or else a failure containing the encountered error.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="action" /> is <see langword="null" />.
        /// </exception> 
        public static Try<T> Do<T>(this Try<T> @try, Action<T> action) {
            action.ThrowIfNull(nameof(action));
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                failure: _ => @try,
                success: x =>
                    Try(() => action(x)).Match(
                        failure: Fail<T>,
                        success: _ => @try));
        }
    }
}