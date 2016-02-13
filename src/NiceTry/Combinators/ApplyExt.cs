using System;
using TheVoid;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}"/> to execute side effects on the value therein.
    /// </summary>
    public static class ApplyExt {
        /// <summary>
        ///     Applies the specified <paramref name="apply" /> to the value of the specified <paramref name="try" /> if that
        ///     represents success. If it represents failure or <paramref name="apply" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="apply"></param>
        /// <exception cref="ArgumentException">
        ///     The property Kind of <paramref name="try"/> is not a valid value of <see cref="TryKind"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
        /// </exception>
        public static Try<Unit> Apply<T>(this Try<T> @try, Action<T> apply) {
            apply.ThrowIfNull(nameof(apply));

            return ApplyWith(@try, x => {
                apply(x);
                return Ok(Unit.Default);
            });
        }

        /// <summary>
        ///     Applies the specified <paramref name="apply" /> to the value of the specified <paramref name="try" /> if that
        ///     represents success. If it represents failure or <paramref name="apply" /> throws an exception, a
        ///     <see cref="Failure{T}" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="try"></param>
        /// <param name="apply"></param>
        /// <exception cref="ArgumentException">
        ///     The property Kind of <paramref name="try"/> is not a valid value of <see cref="TryKind"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="apply" /> is <see langword="null" />.
        /// </exception>
        public static Try<Unit> ApplyWith<T>(this Try<T> @try, Func<T, Try<Unit>> apply) {
            apply.ThrowIfNull(nameof(apply));
            @try.ThrowIfNull(nameof(@try));

            return @try.Match(
                failure: Fail<Unit>,
                success: x => Try(() => apply(x)));
        }
    }
}