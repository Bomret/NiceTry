using System;
using static NiceTry.Predef;

namespace NiceTry.Combinators {
    /// <summary>
    ///     Provides extension methods for <see cref="Try{T}" /> to transform the value therein into new instances. 
    /// </summary>
    public static class SelectManyExt {
        /// <summary>
        ///     Projects the value of the specified <paramref name="try" /> into a new <see cref="Try{T}" />, if it
        ///     represents success.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="try"></param>
        /// <param name="select"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" /> or <paramref name="select" /> is <see langword="null" />.
        /// </exception>
        public static Try<B> SelectMany<A, B>(
            this Try<A> @try,
            Func<A, Try<B>> @select) {
            @try.ThrowIfNull(nameof(@try));
            @select.ThrowIfNull(nameof(@select));

            return @try.Match(
                failure: Fail<B>,
                success: a => Try(() => @select(a)));
        }

        /// <summary>
        ///     Projects the value of the specified <paramref name="try" /> into a new <see cref="Try{T}" />, if it
        ///     represents success and invokes the specified <paramref name="resultSelect" /> on both values.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="try"></param>
        /// <param name="trySelect"></param>
        /// <param name="resultSelect"></param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="try" />, <paramref name="trySelect" /> or <paramref name="resultSelect" /> is <see langword="null" />.
        /// </exception>
        public static Try<C> SelectMany<A, B, C>(
            this Try<A> @try,
            Func<A, Try<B>> trySelect,
            Func<A, B, C> resultSelect) {
            @try.ThrowIfNull(nameof(@try));
            trySelect.ThrowIfNull(nameof(trySelect));
            resultSelect.ThrowIfNull(nameof(resultSelect));

            return @try.Match(
                failure: Fail<C>,
                success: a => Try(() => trySelect(a)).Match(
                    failure: Fail<C>,
                    success: b => Try(() => resultSelect(a, b))));
        }
    }
}